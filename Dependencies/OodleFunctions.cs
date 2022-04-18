using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace DATManager.Dependencies
{
    class OodleFunctions
    {
        public static bool CheckOodleExists()
        {
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "oo2core_8_win64.dll")))
            {
                if (!Properties.Settings.Default.ComplainOodle)
                {
                    MessageBox.Show("Could not locate oo2core_8_win64.dll... LEGO Skywalker Saga archives will not extract correctly!\n Please place oo2core_8_win64.dll in the same directory as this program and restart.", "Missing Oodle DLL!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Properties.Settings.Default.ComplainOodle = true; // We complain once, and then shut up.
                    Properties.Settings.Default.Save();
                }
                return false;
            }
            return true;
        }
    }
}
