using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DATLib;
using DATManager.Dependencies;

namespace DATManager
{
    /// <summary>
    /// Interaction logic for WorkerReport.xaml
    /// </summary>
    public partial class WorkerReport : Window
    {
        Stopwatch timeSpent;

        public WorkerReport()
        {
            InitializeComponent();
            DATInterfacer.workerThread.ProgressChanged += UpdateProgressBar;
            DATInterfacer.workerThread.RunWorkerCompleted += WorkerThread_RunWorkerCompleted;
            StopButton.Click += StopButton_Click;
            timeSpent = new Stopwatch();
            timeSpent.Start();
            DATInterfacer.workerThread.RunWorkerAsync(DATInterfacer.toExtract);
        }

        private void WorkerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timeSpent.Stop();
            ExtractResult result = DATInterfacer.GetResult();
            MessageBox.Show("Successfully extracted " + result.extracted + " / " + result.total + " files!\nTime elapsed: " + (int)(timeSpent.ElapsedMilliseconds / 1000) + " seconds.");
            DATInterfacer.workerThread.ProgressChanged -= UpdateProgressBar;
            DATInterfacer.workerThread.RunWorkerCompleted -= WorkerThread_RunWorkerCompleted;
            Close();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (DATInterfacer.workerThread.IsBusy)
            {
                DATInterfacer.workerThread.CancelAsync();
            }
        }

        private void UpdateProgressBar(object sender, ProgressChangedEventArgs e)
        {
            WorkerProgressBar.Value = e.ProgressPercentage;
            TitleText.Text = (string)e.UserState;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RemoveControls.HideControls(this);
        }
    }
}
