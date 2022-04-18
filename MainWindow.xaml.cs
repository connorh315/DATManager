using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DATLib;
using FileInfo = DATLib.FileInfo;
using DATManager.Dependencies;

namespace DATManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ContextMenuStrip strip;
        private TreeViewItem currentItem;

        public MainWindow()
        {
            InitializeComponent();

            strip = new ContextMenuStrip();
            var item = strip.Items.Add("Extract this item and any contents");
            item.Click += new EventHandler(ExtractItem);

            AllowDrop = true;

            DATInterfacer.SetupWorker();
            DragEnter += new System.Windows.DragEventHandler(Form_DragEnter);
            DragDrop.AddDropHandler(this, Form_DragDrop);

            // Code to compare DATManager results to QuickBMS
            //string managerLocation = @"G:\datmanager";
            //string bmsLocation = @"G:\quickbms";
            //int incorrectFiles = 0;
            //foreach (string file in Directory.GetFiles(managerLocation, "*.*", SearchOption.AllDirectories))
            //{
            //    string rawFile = file.Substring(managerLocation.Length);
            //    Console.WriteLine(rawFile);
            //    byte[] managerFile = File.ReadAllBytes(file);
            //    byte[] bmsFile = File.ReadAllBytes(bmsLocation + rawFile);
            //    for (int i = 0; i < bmsFile.Length; i++)
            //    {
            //        if (managerFile[i] != bmsFile[i])
            //        {
            //            incorrectFiles++;
            //            throw new Exception("not the same at " + rawFile);
            //            break;
            //        }
            //    }
            //}
            //Console.WriteLine(incorrectFiles);
        }

        private static void Form_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop)) e.Effects = System.Windows.DragDropEffects.Copy;
        }

        private static void Form_DragDrop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                ((MainWindow)sender).OpenFile(files[0]);
            }
        }


        private void OpenExtractPicker(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderPicker();
            //dlg.InputPath = @"";
            if (dlg.ShowDialog() == true)
            {
                DATExtract.SetExtractLocation(dlg.ResultPath);
            }
        }

        private void OpenFile(string location)
        {
            DATInterfacer.location = location;
            DATInterfacer.datStructure = null;
            BuildDirectory();
        }

        private void OpenDATPicker(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(DATInterfacer.location);
                openFileDialog.Filter = "DAT Archives (*.DAT)|*.DAT";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OpenFile(openFileDialog.FileName);
                }
            }
        }

        private void ExtractAll(object sender, RoutedEventArgs e)
        {
            DATInterfacer.ExtractAll();
        }

        private object dummyNode = null;

        private void ExtractItem(object sender, EventArgs e)
        {
            if (currentItem.Tag is FileInfo)
            {
                DATInterfacer.ExtractFile((FileInfo)currentItem.Tag);
            }
            else if (currentItem.Tag is DATDirectory)
            {
                DATInterfacer.ExtractDirectory((DATDirectory)currentItem.Tag);
            }
        }

        private void AddSubItem(ItemCollection parent, string childName, object childContainer, bool expandable)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = childName;
            item.Tag = childContainer;
            item.FontWeight = FontWeights.Normal;
            if (expandable)
            {
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folderExpanded);
            }
            item.MouseRightButtonDown += new MouseButtonEventHandler(rightClicked);
            parent.Add(item);
        }

        private void BuildDirectory()
        {
            foldersItem.Items.Clear();
            DATDirectory root = DATInterfacer.GetStructure();
            List<string> names = root.folders.Keys.ToList();
            names.Sort();
            
            foreach (string name in names)
            {
                AddSubItem(foldersItem.Items, name, root.folders[name], true);
            }
            //AddSubItem(foldersItem.Items, entry.Value.name, entry.Value, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            //BuildDirectory();
#endif
            if (!OodleFunctions.CheckOodleExists())
            {
                Title = Title + " - oo2core_8_win64.dll missing!";
            }
        }

        private void rightClicked(object sender, MouseButtonEventArgs e)
        {
            ((TreeViewItem)sender).IsSelected = true;
            e.Handled = true;
            currentItem = (TreeViewItem)sender;
            strip.Show(System.Windows.Forms.Cursor.Position);
        }

        void folderExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    DATDirectory thisView = (DATDirectory)item.Tag;
                    List<string> folderNames = thisView.folders.Keys.ToList();
                    folderNames.Sort();
                    foreach (string folder in folderNames)
                    {
                        AddSubItem(item.Items, folder, thisView.folders[folder], true);
                        //AddSubItem(item.Items, folder.Value.name, folder.Value, true);
                    }
                    List<string> fileNames = thisView.files.Keys.ToList();
                    fileNames.Sort();
                    foreach (string file in fileNames)
                    {
                        AddSubItem(item.Items, file, thisView.files[file], false);
                        //AddSubItem(item.Items, file.Key, file.Value, false);
                    }
                }
                catch (Exception) { }
            }
        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
