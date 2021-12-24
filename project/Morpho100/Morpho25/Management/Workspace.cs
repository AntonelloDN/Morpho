using System;


namespace Morpho25.Management
{
    public enum DatabaseSource
    {
        User,
        Project
    }


    public class Workspace
    {
        public const string DEFAULT_FOLDER = "ENVImet5";
        private const string VERSION = "7174219";
        private const string ENCRIPTION_LEVEL = "5194660";
        private const string CHECK_SUM = "18076640";
        private const string SYSTEM_FOLDER = "sys.basedata";
        private const string USER_FOLDER = "sys.userdata";
        private const string WORKSPACE_INFOX = "workspace.infox";
        private const string PROJECT_INFOX = "project.infox";

        private const string PROJECT_DB = "projectdatabase.edb";
        private const string SYSTEM_DB = "database.edb";
        // TODO: It will change
        private const string USER_DB = "userdatabase.edb";

        private string _workspaceFolder;
        private string _projectFolder;

        public string EnvimetFolder { get; set; }

        private string _systemFolder;
        private string _userFolder;

        public string WorkspaceFolder
        {
            get { return _workspaceFolder; }
            private set
            {
                string folder = GetFolder(value);
                _workspaceFolder = value;
            }
        }

        public string ProjectFolder
        {
            get { return _projectFolder; }
            private set
            {
                string folder = GetFolder(value);
                _projectFolder = value;
            }
        }

        public string ProjectName { get; private set; }
        public string ModelName { get; private set; }
        public string ModelPath { get; private set; }

        public string ProjectDB { get; private set; }
        public string SystemDB { get; private set; }
        public string UserDB { get; private set; }


        private string GetFolder(string folder)
        {
            if (!(System.IO.Directory.Exists(folder)))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            return folder;
        }

        public Workspace(string workspaceFolder, DatabaseSource databaseSource, string envimetFolder = null)
        {
            _systemFolder = GetEnvimetSystemFolder(SYSTEM_FOLDER, envimetFolder);
            _userFolder = GetEnvimetSystemFolder(USER_FOLDER, envimetFolder);

            if (_systemFolder == null)
                throw new ArgumentOutOfRangeException("Envimet not found! Check on your machine or set EnvimetFolder.");

            WorkspaceFolder = workspaceFolder;
            ProjectName = "Project";
            ModelName = "Model";

            ProjectFolder = System.IO.Path.Combine(WorkspaceFolder, ProjectName);
            ModelPath = System.IO.Path.Combine(ProjectFolder, ModelName + ".inx");

            string workspaceInfoxAbsPath = System.IO.Path.Combine(_systemFolder, WORKSPACE_INFOX);
            string projectInfoxAbsPath = System.IO.Path.Combine(ProjectFolder, PROJECT_INFOX);

            SetWorkSpaceInfox(workspaceInfoxAbsPath);
            SetProjectInfox(projectInfoxAbsPath, databaseSource);

            SetDatabase(databaseSource, _systemFolder, _userFolder);
        }

        public Workspace(string workspaceFolder, DatabaseSource databaseSource, string projectName, string modelName, string envimetFolder)
            : this(workspaceFolder, databaseSource, envimetFolder)
        {
            ProjectName = projectName;
            ProjectFolder = System.IO.Path.Combine(WorkspaceFolder, ProjectName);
            ModelName = modelName;
            ModelPath = System.IO.Path.Combine(ProjectFolder, ModelName + ".inx");

            string workspaceInfoxAbsPath = System.IO.Path.Combine(_systemFolder, WORKSPACE_INFOX);
            string projectInfoxAbsPath = System.IO.Path.Combine(ProjectFolder, PROJECT_INFOX);

            SetWorkSpaceInfox(workspaceInfoxAbsPath);
            SetProjectInfox(projectInfoxAbsPath, databaseSource);

            SetDatabase(databaseSource, _systemFolder, _userFolder);
        }

        public override string ToString()
        {
            return String.Format("Workspace::{0}::{1}", ProjectName, ModelName);
        }

        private void SetDatabase(DatabaseSource databaseSource, string systemFolder, string userFolder)
        {
            SystemDB = System.IO.Path.Combine(systemFolder, SYSTEM_DB);

            if (databaseSource == DatabaseSource.Project)
            {
                ProjectDB = System.IO.Path.Combine(ProjectFolder, PROJECT_DB);
                if (!System.IO.File.Exists(ProjectDB))
                    SetProjectDB();
            }
            else
            {
                UserDB = System.IO.Path.Combine(userFolder, USER_DB);
            }
        }

        private string GetEnvimetSystemFolder(string folderName, string envimetFolder)
        {
            string root = System.IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string directory = System.IO.Path.Combine(root, DEFAULT_FOLDER + $"\\{folderName}\\");

            if (envimetFolder != null)
                directory = System.IO.Path.Combine(envimetFolder, $"{folderName}\\");

            if (System.IO.Directory.Exists(directory))
                return directory;
            else
                return null;
        }

        private void SetWorkSpaceInfox(string workspaceInfoxAbsPath)
        {
            var now = DateTime.Now;

            string[] textWorkspaceInfox = {
                "<ENVI-MET_Datafile>",
                "<Header>",
                "<filetype>workspacepointer</filetype>",
                $"<version>{VERSION}</version>",
                $"<revisiondate>{now.ToString("yyyy-MM-dd HH:mm:ss")}</revisiondate>",
                "<remark></remark>",
                $"<checksum>{CHECK_SUM}</checksum>",
                $"<encryptionlevel>{ENCRIPTION_LEVEL}</encryptionlevel>",
                "</Header>",
                "<current_workspace>",
                $"<absolute_path> {WorkspaceFolder} </absolute_path>",
                $"<last_active> {ProjectName} </last_active>",
                "</current_workspace>",
                "</ENVI-MET_Datafile>"
            };

            System.IO.File.WriteAllLines(workspaceInfoxAbsPath, textWorkspaceInfox);
        }

        private void SetProjectDB()
        {
            var now = DateTime.Now;

            string[] dbText = {
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

            System.IO.File.WriteAllLines(ProjectDB, dbText);
        }

        private void SetProjectInfox(string projectInfoxAbsPath, DatabaseSource databaseSource)
        {
            var now = DateTime.Now;

            string[] textProjectFileInfox = {
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

            System.IO.File.WriteAllLines(projectInfoxAbsPath, textProjectFileInfox);
        }
    }
}
