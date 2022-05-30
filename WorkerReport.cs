using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DATManager
{
    public partial class WorkerReport : Form
    {
        Stopwatch timeSpent;

        string nextExtraction = "";

        public WorkerReport()
        {
            InitializeComponent();
            DATInterfacer.workerThread.ProgressChanged += new ProgressChangedEventHandler(UpdateProgressBar);
            DATInterfacer.workerThread.RunWorkerCompleted += WorkerThread_RunWorkerCompleted;

            timeSpent = new Stopwatch();
            timeSpent.Start();
            DATInterfacer.workerThread.RunWorkerAsync(DATInterfacer.toExtract);
        }

        private void WorkerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timeSpent.Stop();
            int[] result = DATInterfacer.GetResult();
            MessageBox.Show("Successfully extracted " + result[0] + " / " + result[1] + " files!\nTime elapsed: " + (int)(timeSpent.ElapsedMilliseconds / 1000) + " seconds.");
            DATInterfacer.workerThread.ProgressChanged -= UpdateProgressBar;
            DATInterfacer.workerThread.RunWorkerCompleted -= WorkerThread_RunWorkerCompleted;
            Close();
        }

        private void UpdateProgressBar(object sender, ProgressChangedEventArgs e)
        {
            // This thing is so fast now that it updates quicker than the UI can catch up.
            if (e.ProgressPercentage % 2 == 0)
            {
                progressBar1.Value = e.ProgressPercentage;
                progressBar1.Refresh();
            }
            nextExtraction = (string)e.UserState;
        }



        private void WorkerReport_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DATInterfacer.workerThread.IsBusy)
            {
                DATInterfacer.workerThread.CancelAsync();
            }
        }
    }
}
