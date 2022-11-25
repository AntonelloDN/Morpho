using Morpho25.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morpho25.IO
{
    /// <summary>
    /// Climate file batch class.
    /// </summary>
    class FoxBatch
    {
        /// <summary>
        /// Generate a climate file from a EPW file.
        /// </summary>
        /// <param name="epw">EPW file to use.</param>
        /// <param name="workspace">Workspace.</param>
        /// <returns></returns>
        public static string GetFoxFile(string epw, 
            Workspace workspace)
        {
            string envimet;
            string root = System.IO.Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            if (workspace.EnvimetFolder == null)
                envimet = System.IO.Path.Combine(root, Workspace.DEFAULT_FOLDER + "\\win64");
            else
                envimet = System.IO.Path.Combine(workspace.EnvimetFolder, "win64");

            string foxName = System.IO.Path.GetFileNameWithoutExtension(epw) + ".fox";
            string target = System.IO.Path.Combine(workspace.ProjectFolder, foxName);

            string path = System.IO.Path.Combine(workspace.ProjectFolder, workspace.ModelName + "FOX.bat");

            string batch = "@echo I'm writing FOX file...\n" +
            "@echo off\n" +
            "cd {0}\n" +
            "if errorlevel 1 goto :failed\n" +
            "foxmanager.exe {1} {2}\n" +
            ": failed\n" +
            "echo If Envimet is not in default unit 'C:\' connect installation folder.\n" +
            "pause\n";

            string[] contentOfBatch = { String.Format(batch, envimet, epw, target) };

            System.IO.File.WriteAllLines(path, contentOfBatch);

            if (!System.IO.File.Exists(target))
                RunBat(path);

            return foxName;
        }

        private static void RunBat(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = path;
            process.Start();
        }
    }
}
