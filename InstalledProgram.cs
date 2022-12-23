﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCollectInfoApp
{
    public class InstalledProgram
    {
        public string? DisplayName { get; set; }
        public string? Version { get; set; }
        public string? InstalledDate { get; set; }
        public string? Publisher { get; set; }
        public string? UnninstallCommand { get; set; }
        public string? ModifyPath { get; set; }
    }
}
