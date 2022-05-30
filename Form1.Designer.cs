namespace DATManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openArchive = new System.Windows.Forms.ToolStripDropDownButton();
            this.openDATFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderOfArchivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.extractEverythingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setExtractLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datFileTree = new System.Windows.Forms.TreeView();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openArchive,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openArchive
            // 
            this.openArchive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openArchive.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDATFileToolStripMenuItem,
            this.openFolderOfArchivesToolStripMenuItem});
            this.openArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openArchive.Name = "openArchive";
            this.openArchive.Size = new System.Drawing.Size(46, 24);
            this.openArchive.Text = "File";
            // 
            // openDATFileToolStripMenuItem
            // 
            this.openDATFileToolStripMenuItem.Name = "openDATFileToolStripMenuItem";
            this.openDATFileToolStripMenuItem.Size = new System.Drawing.Size(265, 26);
            this.openDATFileToolStripMenuItem.Text = "Open .DAT archive";
            this.openDATFileToolStripMenuItem.Click += new System.EventHandler(this.openDATFileToolStripMenuItem_Click);
            // 
            // openFolderOfArchivesToolStripMenuItem
            // 
            this.openFolderOfArchivesToolStripMenuItem.Name = "openFolderOfArchivesToolStripMenuItem";
            this.openFolderOfArchivesToolStripMenuItem.Size = new System.Drawing.Size(265, 26);
            this.openFolderOfArchivesToolStripMenuItem.Text = "Open all archives in folder";
            this.openFolderOfArchivesToolStripMenuItem.Click += new System.EventHandler(this.openFolderOfArchivesToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractEverythingToolStripMenuItem,
            this.setExtractLocationToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(68, 24);
            this.toolStripDropDownButton2.Text = "Extract";
            // 
            // extractEverythingToolStripMenuItem
            // 
            this.extractEverythingToolStripMenuItem.Name = "extractEverythingToolStripMenuItem";
            this.extractEverythingToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.extractEverythingToolStripMenuItem.Text = "Extract everything";
            this.extractEverythingToolStripMenuItem.Click += new System.EventHandler(this.extractEverythingToolStripMenuItem_Click);
            // 
            // setExtractLocationToolStripMenuItem
            // 
            this.setExtractLocationToolStripMenuItem.Name = "setExtractLocationToolStripMenuItem";
            this.setExtractLocationToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.setExtractLocationToolStripMenuItem.Text = "Set extract location";
            this.setExtractLocationToolStripMenuItem.Click += new System.EventHandler(this.setExtractLocationToolStripMenuItem_Click);
            // 
            // datFileTree
            // 
            this.datFileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datFileTree.Location = new System.Drawing.Point(0, 54);
            this.datFileTree.Name = "datFileTree";
            this.datFileTree.Size = new System.Drawing.Size(800, 396);
            this.datFileTree.TabIndex = 1;
            this.datFileTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.datFileTree_BeforeExpand);
            this.datFileTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.datFileTree_AfterSelect);
            this.datFileTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.datFileTree_NodeMouseClick);
            // 
            // searchBox
            // 
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBox.Location = new System.Drawing.Point(0, 27);
            this.searchBox.Name = "searchBox";
            this.searchBox.PlaceholderText = "Search";
            this.searchBox.Size = new System.Drawing.Size(800, 27);
            this.searchBox.TabIndex = 2;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.datFileTree);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "DATManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton openArchive;
        private ToolStripMenuItem openDATFileToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem extractEverythingToolStripMenuItem;
        private TreeView datFileTree;
        private ToolStripMenuItem openFolderOfArchivesToolStripMenuItem;
        private ToolStripMenuItem setExtractLocationToolStripMenuItem;
        private TextBox textBox1;
        private TextBox searchBox;
    }
}