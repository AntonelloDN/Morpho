using Morpho25.Management;
using System;
using System.Diagnostics;
using System.IO;

namespace Morpho25.IO
{
    /// <summary>
    /// Simulation batch class.
    /// </summary>
    public class SimulationBatch
    {
        /// <summary>
        /// Run envimet simulation.
        /// </summary>
        /// <param name="simx">Simulation definition.</param>
        /// <exception cref="Exception"></exception>
        public static void RunSimulation(Simx simx)
        {
            try
            {
                Process.Start(GetBatchFile(simx));
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new Exception("Batch file not found. " +
                    "If problem persist, run simulation using ENVI-Met GUI.");
            }
        }

        private static string GetBatchFile(Simx simx)
        {
            string envimet;
            string root = Path.GetPathRoot(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData));

            if (simx.MainSettings.Inx.Workspace.EnvimetFolder == null)
                envimet = Path.Combine(root, 
                    Workspace.DEFAULT_FOLDER + "\\win64");
            else
                envimet = Path.Combine(simx.MainSettings
                    .Inx.Workspace.EnvimetFolder, "win64");

            string project = simx.MainSettings.Inx.Workspace.ProjectName;
            string simulationName = simx.MainSettings.Name + ".simx";

            string path = Path.Combine(simx.MainSettings
                .Inx.Workspace.ProjectFolder, simulationName + ".bat");
            string unit = Path.GetPathRoot(envimet);
            unit = Path.GetPathRoot(envimet).Remove(unit.Length - 1);

            string batch = $"@echo off\n" +
            $"cd {unit}\n" +
            $"cd {envimet}\n" +
            $"if errorlevel 1 goto :failed\n" +
            $"\"{envimet}\\envicore_console.exe\" \"{simx.MainSettings.Inx.Workspace.WorkspaceFolder}\" \"{project}\" \"{simulationName}\"\n" +
            $": failed\n" +
            $"echo If Envimet is not in default unit 'C:\' connect installation folder.\n" +
            $"pause\n";

            //string[] contentOfBatch = { String.Format(batch, unit, envimet, project, simulationName) };

            File.WriteAllText(path, batch);

            return path;
        }
    }
}
