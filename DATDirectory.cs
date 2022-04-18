using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATLib;

namespace DATManager
{
    public class DATDirectory
    {
        public string name;
        public Dictionary<string, FileInfo> files = new Dictionary<string, FileInfo>();
        public Dictionary<string, DATDirectory> folders = new Dictionary<string, DATDirectory>();
        
        public DATDirectory(string directoryName)
        {
            name = directoryName;
        }

        public DATDirectory CreateNewDirectory(string directoryName)
        {
            DATDirectory newDirectory = new DATDirectory(directoryName);
            folders.Add(directoryName, newDirectory);
            return newDirectory;
        }
    }
}
