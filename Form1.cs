using DATManager.Invocations;
using DATExtract;
using System.Diagnostics;

namespace DATManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cms.Items.Add("Extract this item and any contents", null, new EventHandler(ExtractItem));
            DATInterfacer.SetupWorker();

            if (!OodleFunctions.CheckOodleExists())
            {
                Text = Text + " - oo2core_8_win64.dll missing!";
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void openDATFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "DAT Archives (*.DAT)|*.DAT";
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(fileDialog.FileName);
                }
            }
        }

        private void OpenFolder(string folderLocation)
        {
            DATInterfacer.Reset();
            string[] allFiles = Directory.GetFiles(folderLocation, "*.dat");
            Console.WriteLine("Found {0} DAT files!", allFiles.Length);
            foreach (string file in allFiles)
            {
                string rawName = file.Substring(folderLocation.Length + 1);
                if (rawName.StartsWith("LP"))
                {
                    Console.WriteLine("Ignoring {0} as it is a language pack", rawName);
                    continue;
                }
                DATInterfacer.Add(file);
            }

            Populate(DATInterfacer.GetStructure());
        }

        private void openFolderOfArchivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FolderPicker();
            if (dialog.ShowDialog(Handle) == true)
            {
                OpenFolder(dialog.ResultPath);
            }
        }

        private void Populate(DATDirectory root)
        {
            datFileTree.BeginUpdate();

            datFileTree.Nodes.Clear();
            List<string> names = root.folders.Keys.ToList();
            names.Sort();

            foreach (string name in names)
            {
                AddItem(datFileTree.Nodes, name, root.folders[name], true);
            }

            List<string> files = root.files.Keys.ToList();
            files.Sort();

            foreach (string name in files)
            {
                AddItem(datFileTree.Nodes, name, root.files[name], false);
            }

            datFileTree.EndUpdate();
        }

        private void OpenFile(string filename)
        {
            FileAttributes attr = File.GetAttributes(filename);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                Console.WriteLine("Assuming folder contains .DAT archives");
                OpenFolder(filename);
            }
            else
            {
                DATInterfacer.Open(filename);
            }

            Populate(DATInterfacer.GetStructure());
        }

        private void AddItem(TreeNodeCollection parent, string childName, object childContainer, bool expandable)
        {
            TreeNode node = parent.Add(childName);
            node.Tag = childContainer;
            if (expandable)
            {
                node.Nodes.Add("");
            }
        }

        private void datFileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void ExtractItem(object sender, EventArgs e)
        {
            ContextMenuStrip cms = (ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent();
            if (cms.Tag is CompFileReference)
            {
                DATInterfacer.ExtractFile((CompFileReference)cms.Tag);
            }
            else if (cms.Tag is DATDirectory)
            {
                DATInterfacer.ExtractDirectory((DATDirectory)cms.Tag);
            }
        }

        private void datFileTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "")
            {
                e.Node.Nodes.Clear();

                DATDirectory thisView = (DATDirectory)e.Node.Tag;
                List<string> folderNames = thisView.folders.Keys.ToList();
                folderNames.Sort();
                foreach (string folder in folderNames)
                {
                    AddItem(e.Node.Nodes, folder, thisView.folders[folder], true);
                }
                List<string> fileNames = thisView.files.Keys.ToList();
                fileNames.Sort();
                foreach (string file in fileNames)
                {
                    AddItem(e.Node.Nodes, file, thisView.files[file], false);
                }
            }
        }

        internal ContextMenuStrip cms = new ContextMenuStrip();

        private void datFileTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                e.Node.ContextMenuStrip = cms;
                cms.Tag = e.Node.Tag;
                cms.Show();
            }
        }

        private void extractEverythingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DATInterfacer.ExtractAll();
        }

        private void setExtractLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FolderPicker();
            if (dialog.ShowDialog(Handle) == true)
            {
                DATInterfacer.extractLocation = dialog.ResultPath;
                DATInterfacer.extractUpdatedByUser = true;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                OpenFile(files[0]);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void extractMultipleArchivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private Dictionary<CompFile, int> FindFiles(string filename)
        {
            Dictionary<CompFile, int> filesFound = new Dictionary<CompFile, int>();
            for (int i = 0; i < DATInterfacer.datFiles.Count; i++)
            {
                foreach (CompFile file in DATInterfacer.datFiles[i].files)
                {
                    if (file.path.Contains(filename))
                    {
                        filesFound.Add(file, i);
                        //Console.WriteLine(file.path);
                    }
                }
            }

            return filesFound;
        }

        bool searching = false;

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text.Length > 4)
            {
                Dictionary<CompFile, int> filesFound = FindFiles(searchBox.Text);
                Console.WriteLine("Found {0} files that contain query", filesFound.Count);
                DATDirectory datStructure = new DATDirectory("root");
                foreach (KeyValuePair<CompFile, int> fileStruct in filesFound)
                {
                    CompFile file = fileStruct.Key;
                    string[] root = file.path.Split(Path.DirectorySeparatorChar);
                    DATDirectory prevDirectory = datStructure;
                    for (int i = 1; i < root.Length; i++) // Start at 1, since the 0 element is empty since the string starts with \
                    {
                        string entry = root[i];
                        if (i == root.Length - 1)
                        {
                            prevDirectory.files[entry] = new CompFileReference(file, fileStruct.Value);
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
                searching = true;
                Populate(datStructure);
            }
            else
            {
                if (searching == true)
                {
                    searching = false;
                    Populate(DATInterfacer.GetStructure());
                }
            }
            //Console.WriteLine(searchBox.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}