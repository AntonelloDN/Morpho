using System;
using System.IO;
using System.Reflection;

namespace Morpho25.Management
{
    /// <summary>
    /// Database source enum.
    /// </summary>
    public enum DatabaseSource
    {
        User,
        Project
    }

    /// <summary>
    /// Workspace class.
    /// </summary>
    public class Workspace
    {
        /// <summary>
        /// Default envimet folder.
        /// </summary>
        public const string DEFAULT_FOLDER = "ENVImet5";
        #region System details
        private const string VERSION = "7174219";
        private const string ENCRIPTION_LEVEL = "5194660";
        private const string CHECK_SUM = "18076640";
        private const string SYSTEM_FOLDER = "sys.basedata";
        private const string PYTHON_FOLDER = "sys.python";
        #endregion

        #region User details
        private const string USER_FOLDER = "sys.userdata";
        private const string USER_SETTINGS = "usersettings.setx";
        private const string USER_DB = "userdatabase.edb";
        public const string FOLDER_NAME = "ENVI-met";
        #endregion

        #region Project details
        private const string PROJECT_INFOX = "project.infox";
        private const string PROJECT_DB = "projectdatabase.edb";
        private const string SYSTEM_DB = "database.edb";
        #endregion

        #region Main paths
        private string _workspaceFolder;
        private string _projectFolder;
        private string _pythonFolder;
        #endregion

        #region DB paths
        private string _systemFolder;
        private string _userFolder;
        #endregion

        public string EnvimetFolder { get; set; }

        /// <summary>
        /// Workspace folder.
        /// </summary>
        public string WorkspaceFolder
        {
            get { return _workspaceFolder; }
            private set
            {
                string folder = GetFolder(value);
                _workspaceFolder = folder;
            }
        }
        /// <summary>
        /// Project folder.
        /// </summary>
        public string ProjectFolder
        {
            get { return _projectFolder; }
            private set
            {
                string folder = GetFolder(value);
                _projectFolder = value;
            }
        }
        /// <summary>
        /// Project name.
        /// </summary>
        public string ProjectName { get; private set; }
        /// <summary>
        /// Model name.
        /// </summary>
        public string ModelName { get; private set; }
        /// <summary>
        /// Model path.
        /// </summary>
        public string ModelPath { get; private set; }
        /// <summary>
        /// Project database location.
        /// </summary>
        public string ProjectDB { get; private set; }
        /// <summary>
        /// System database location.
        /// </summary>
        public string SystemDB { get; private set; }
        /// <summary>
        /// User database location.
        /// </summary>
        public string UserDB { get; private set; }

        private string GetFolder(string folder)
        {
            if (!(Directory.Exists(folder)))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }
        /// <summary>
        /// Create a workspace object.
        /// </summary>
        /// <param name="workspaceFolder">Workspace folder.</param>
        /// <param name="databaseSource">Database source.</param>
        /// <param name="envimetFolder">Envimet folder on machine.</param>
        /// <exception cref="ArgumentOutOfRangeException">Envimet not found.</exception>
        public Workspace(string workspaceFolder, 
            DatabaseSource databaseSource, 
            string envimetFolder = null)
        {
            _systemFolder = GetEnvimetSystemFolder(SYSTEM_FOLDER, envimetFolder);
            _userFolder = GetEnvimetSystemFolder(USER_FOLDER, envimetFolder);
            _pythonFolder = GetEnvimetSystemFolder(PYTHON_FOLDER, envimetFolder);

            if (_systemFolder == null)
                throw new ArgumentOutOfRangeException("Envimet not found! " +
                    "Check on your machine or set EnvimetFolder.");

            WorkspaceFolder = workspaceFolder;
            ProjectName = "Project";
            ModelName = "Model";

            ProjectFolder = Path.Combine(WorkspaceFolder, ProjectName);
            ModelPath = Path.Combine(ProjectFolder, ModelName + ".inx");

            string projectInfoxAbsPath = Path.Combine(ProjectFolder, PROJECT_INFOX);

            SetUserSettings();
            SetProjectInfox(projectInfoxAbsPath, databaseSource);

            SetDatabase(databaseSource, _systemFolder, _userFolder);
        }
        /// <summary>
        /// Create a workspace object.
        /// </summary>
        /// <param name="workspaceFolder">Workspace folder.</param>
        /// <param name="databaseSource">Database source.</param>
        /// <param name="projectName">Project name.</param>
        /// <param name="modelName">Model name.</param>
        /// <param name="envimetFolder">Envimet folder.</param>
        public Workspace(string workspaceFolder, DatabaseSource databaseSource, 
            string projectName, string modelName, string envimetFolder)
            : this(workspaceFolder, databaseSource, envimetFolder)
        {
            ProjectName = projectName;
            ProjectFolder = Path.Combine(WorkspaceFolder, ProjectName);
            ModelName = modelName;
            ModelPath = Path.Combine(ProjectFolder, ModelName + ".inx");

            string projectInfoxAbsPath = Path.Combine(ProjectFolder, PROJECT_INFOX);

            SetUserSettings();
            SetProjectInfox(projectInfoxAbsPath, databaseSource);

            SetDatabase(databaseSource, _systemFolder, _userFolder);
        }
        /// <summary>
        /// String representation of the workspace object.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return String.Format("Workspace::{0}::{1}", 
                ProjectName, ModelName);
        }

        private void SetDatabase(DatabaseSource databaseSource, 
            string systemFolder, string userFolder)
        {
            SystemDB = Path.Combine(systemFolder, SYSTEM_DB);

            if (databaseSource == DatabaseSource.Project)
            {
                ProjectDB = Path.Combine(ProjectFolder, PROJECT_DB);
                if (!File.Exists(ProjectDB))
                    SetProjectDB();
            }
            else
            {
                UserDB = Path.Combine(userFolder, USER_DB);
            }
        }

        private string GetEnvimetSystemFolder(string folderName, 
            string envimetFolder)
        {
            string root = System.IO.Path.GetPathRoot(Environment
                .GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string directory = System.IO.Path.Combine(root, 
                DEFAULT_FOLDER + $"\\{folderName}\\");

            if (envimetFolder != null)
                directory = System.IO.Path.Combine(envimetFolder, $"{folderName}\\");

            if (System.IO.Directory.Exists(directory))
                return directory;
            else
                return null;
        }

        private void SetUserSettings()
        {
            var folder = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);
            var targetFolder = Path.Combine(folder, FOLDER_NAME);
            var targetFile = Path.Combine(targetFolder, USER_SETTINGS);

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            WriteUserSettings(targetFile);
        }

        private void WriteUserSettings(string userSettingsAbsPath)
        {
            var now = DateTime.Now;

            string[] userSettingsData = {
                "<ENVI-MET_Datafile>",
                "<Header>",
                "<filetype>SETX ENVI-met User Settings</filetype>",
                $"<version>{VERSION}</version>",
                $"<revisiondate>{now.ToString("yyyy-MM-dd HH:mm: ss")}</revisiondate>",
                "<remark></remark>",
                "<checksum>0</checksum>",
                "<encryptionlevel>0</encryptionlevel>",
                "</Header>",
                "<current_workspace>",
                $"<absolute_path> {WorkspaceFolder} </absolute_path>",
                $"<last_active> {ProjectName} </last_active>",
                "</current_workspace>",
                "<python_settings>",
                $"<selectedPython> {_pythonFolder} </selectedPython>",
                "</python_settings>",
                "<userpathinfo>",
                "<userpathmode> 1 </userpathmode>",
                $"<userpathinfo> {_userFolder} </userpathinfo>",
                "</userpathinfo>",
                "</ENVI-MET_Datafile>"
            };

            File.WriteAllLines(userSettingsAbsPath, userSettingsData);
        }

        private void SetProjectDB()
        {
            var now = DateTime.Now;

            string[] projectDBdata = {
                "<ENVI-MET_Datafile>",
                "<Header>",
                "<filetype>DATA</filetype>",
                "<version>1</version>",
                String.Format("<revisiondate>{0}</revisiondate>", 
                now.ToString("yyyy-MM-dd HH:mm:ss")),
                "<remark>Envi-Data</remark>",
                "<checksum>0</checksum>",
                "<encryptionlevel>1</encryptionlevel>",
                "</Header>",
                "</ENVI-MET_Datafile>"
            };

            File.WriteAllLines(ProjectDB, projectDBdata);
        }

        private void SetProjectInfox(string projectInfoxAbsPath, DatabaseSource databaseSource)
        {
            var now = DateTime.Now;

            string[] projectInfoData = {
                "<ENVI-MET_Datafile>",
                "<Header>",
                "<filetype>infoX ENVI-met Project Description File</filetype>",
                $"<version>{VERSION}</version>",
                $"<revisiondate>{now.ToString("yyyy-MM-dd HH:mm:ss")}</revisiondate>",
                "<remark></remark>",
                $"<checksum>{CHECK_SUM}</checksum>",
                $"<encryptionlevel>{ENCRIPTION_LEVEL}</encryptionlevel>",
                "</Header>",
                "<project_description>",
                $"<name> {ProjectName} </name>",
                "<description>  </description>",
                $"<useProjectDB> {(int) databaseSource} </useProjectDB>",
                "</project_description>",
                "</ENVI-MET_Datafile>"
            };

            File.WriteAllLines(projectInfoxAbsPath, projectInfoData);
        }
    }
}
