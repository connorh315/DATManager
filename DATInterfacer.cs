using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DATExtract;

namespace DATManager
{
    internal static class DATInterfacer
    {
        internal static string extractLocation = null;

        private static DATDirectory datStructure;

        internal static int[] GetResult()
        {
            int[] result = new int[2];
            for (int i = 0; i < datFiles.Count; i++)
            {
                result[0] += datFiles[i].successfullyExtracted;
                result[1] += datFiles[i].extractAmount;
            }
            return result;
        }

        public static void GetFromFiles(DATDirectory datStructure, CompFile[] files, int datReference)
        {
            foreach (CompFile file in files)
            {
                if (file.path == null) continue;
                string[] root = file.path.Split(Path.DirectorySeparatorChar);
                DATDirectory prevDirectory = datStructure;
                for (int i = 1; i < root.Length; i++) // Start at 1, since the 0 element is empty since the string starts with \
                {
                    string entry = root[i];
                    if (i == root.Length - 1)
                    {
                        prevDirectory.files[entry] = new CompFileReference(file, datReference);
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
        }

        internal static DATDirectory GetStructure()
        {
            if (datStructure != null)
            {
                return datStructure;
            }

            datStructure = new DATDirectory("root");

            for (int j = 0; j < datFiles.Count; j++)
            {
                DATFile datFile = datFiles[j];

                CompFile[] files = datFile.files;

                GetFromFiles(datStructure, files, j);
            }

            return datStructure;
        }

        internal static void Reset()
        {
            datFiles.Clear();
            datStructure = null;
        }

        internal static bool extractUpdatedByUser = false;

        internal static void Open(string filename)
        {
            Reset();
            DATFile datFile = DATFile.Open(filename);
            datFiles.Add(datFile);
            
            if (extractUpdatedByUser == false)
            {
                extractLocation = Path.GetDirectoryName(filename);
            }
        }

        internal static List<DATFile> datFiles = new List<DATFile>();

        internal static void Add(string filename)
        {
            DATFile datFile = DATFile.Open(filename);
            datFiles.Add(datFile);

            if (extractUpdatedByUser == false)
            {
                extractLocation = Path.GetDirectoryName(filename);
            }
        }

        internal static object toExtract = null;
        private static List<CompFileReference> congregate = new List<CompFileReference>();

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

            congregate = new List<CompFileReference>(); // Dispose of previous list
        }

        public static void ExtractFile(CompFileReference file)
        {
            toExtract = file;
            LoadReporter();
        }

        public static void ExtractAll()
        {
            toExtract = null;
            LoadReporter();
        }

        public static BackgroundWorker workerThread = new BackgroundWorker();

        private static bool workerSetup = false;
        public static void SetupWorker()
        {
            if (workerSetup) return;

            workerThread.DoWork += new DoWorkEventHandler(RunExtractor);
            workerThread.WorkerReportsProgress = true;
            workerThread.WorkerSupportsCancellation = true;
        }

        private static void LoadReporter()
        {
            WorkerReport modal = new WorkerReport();
            modal.ShowDialog();
        }

        private static void RunExtractor(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is CompFileReference[])
            {
                CompFileReference[] compFiles = (CompFileReference[])e.Argument;
                List<CompFile>[] sortedFiles = new List<CompFile>[datFiles.Count];
                for (int i = 0; i < datFiles.Count; i++)
                {
                    sortedFiles[i] = new List<CompFile>();
                }

                foreach (CompFileReference compFile in compFiles)
                {
                    sortedFiles[compFile.datID].Add(compFile.file);
                }
                
                for (int i = 0; i < datFiles.Count; i++)
                {
                    if (sortedFiles[i].Count > 0)
                    {
                        Console.WriteLine("Extracting from {0}", Path.GetFileNameWithoutExtension(datFiles[i].fileLocation));
                        datFiles[i].ExtractCollection(sortedFiles[i].ToArray(), extractLocation, workerThread);
                    } 
                }
            }
            else if (e.Argument is CompFileReference)
            {
                CompFileReference compFile = (CompFileReference)e.Argument;
                datFiles[compFile.datID].ExtractFile(compFile.file, extractLocation);
            }
            else
            {
                for (int i = 0; i < datFiles.Count; i++)
                {
                    Console.WriteLine("Extracting {0}", Path.GetFileNameWithoutExtension(datFiles[i].fileLocation));
                    datFiles[i].ExtractAll(extractLocation, workerThread);
                }
            }
        }
    }
}
