using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCollectInfoApp
{
    public class KeysPath
    {
        public KeysPath(string path)
        {
            this.Path = path;
        }
        public string Path { get; private set; }
    }
}
