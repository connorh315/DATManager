using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DATLib;
using FileInfo = DATLib.FileInfo;
using System.ComponentModel;
using System.Windows;

namespace DATManager
{
    internal static class DATInterfacer
    {
        internal static DATDirectory datStructure;
#if DEBUG
        //internal static string location = @"D:\Games\SteamLibrary\steamapps\common\LEGO Star Wars - The Skywalker Saga\GAME.DAT";
        //internal static string location = @"A:\Projects\DLC2.DAT";
        internal static string location = @"";
#else
        internal static string location = @"";
#endif
        public static BackgroundWorker workerThread = new BackgroundWorker();

        internal static object toExtract = null;

        private static void RunExtractor(object sender, DoWorkEventArgs e)
        {

            if (e.Argument is FileInfo[])
            {
                DATExtract.ExtractFiles((FileInfo[])e.Argument, workerThread);
            }
            else if (e.Argument is FileInfo)
            {
                DATExtract.ExtractFile((FileInfo)e.Argument);
            }
            else
            {
                DATExtract.ExtractAll(workerThread);
            }
        }

        public static DATDirectory GetStructure()
        {
            if (datStructure != null)
            {
                return datStructure;
            }

            datStructure = new DATDirectory("root");
            DATExtract.SetDAT(location);
            FileInfo[] files = DATExtract.GetFiles();

            foreach (FileInfo file in files)
            {
                string[] root = file.path.Split(Path.DirectorySeparatorChar);
                DATDirectory prevDirectory = datStructure;
                for (int i = 1; i < root.Length; i++) // Start at 1, since the 0 element is empty since the string starts with \
                {
                    string entry = root[i];
                    if (i == root.Length - 1)
                    {
                        prevDirectory.files[entry] = file;
                    }
                    else if (!prevDirectory.folders.ContainsKey(entry))
                    {
                        DATDirectory newEntry = prevDirectory.CreateNewDirectory(entry);
                        prevDirectory = newEntry;
                    }
                    else
                    {
                        prevDirectory = prevDirectory.folders[entry];
                    }
                }
            }

            return datStructure;
        }

        private static void LoadReporter()
        {
            WorkerReport modal = new WorkerReport();
            modal.ShowDialog();
        }

        public static void ExtractFile(FileInfo file)
        {
            toExtract = file;
            LoadReporter();
        }

        public static void ExtractAll()
        {
            toExtract = null;
            LoadReporter();
        }

        private static List<FileInfo> congregate = new List<FileInfo>();

        private static void Marshal(DATDirectory directory)
        {
            foreach (var entry in directory.files)
            {
                congregate.Add(entry.Value);
            }
            foreach (var entry in directory.folders)
            {
                Marshal(entry.Value);
            }
        }

        public static void ExtractDirectory(DATDirectory directory)
        {
            Marshal(directory);

            toExtract = congregate.ToArray();
            LoadReporter();
            //workerThread.RunWorkerAsync(congregate.ToArray());

            congregate = new List<FileInfo>(); // Dispose of previous list
        }

        private static bool workerSetup = false;
        public static void SetupWorker()
        {
            if (workerSetup) return;

            workerThread.DoWork += new DoWorkEventHandler(RunExtractor);
            workerThread.WorkerReportsProgress = true;
            workerThread.WorkerSupportsCancellation = true;
        }

        public static ExtractResult GetResult()
        {
            return DATExtract.GetResult();
        }

        public static string[] GetFailedFiles()
        {
            return DATExtract.GetFailedFiles();
        }
    }
}
