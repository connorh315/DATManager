using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATExtract;

namespace DATManager
{
    public class CompFileReference
    {
        public CompFile file;
        public int datID;

        public CompFileReference(CompFile file, int datID)
        {
            this.file = file;
            this.datID = datID;
        }
    }

    internal class DATDirectory
    {
        public string name;
        public Dictionary<string, CompFileReference> files = new Dictionary<string, CompFileReference>();
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
