using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCollectInfoApp
{
    public static class SystemLogic
    {
        public static List<InstalledProgram> FindInstalls(RegistryKey regKey, List<string> keys)
        {
            List<InstalledProgram> installed = new();
            foreach (string key in keys)
            {
                if (string.IsNullOrEmpty(key)) continue;

                using RegistryKey? rk = regKey.OpenSubKey(key);
                if (rk == null) continue;
                
                foreach (string skName in rk.GetSubKeyNames())
                {
                    if (string.IsNullOrEmpty(skName)) continue;
                    using RegistryKey? sk = rk.OpenSubKey(skName);
                    if (sk == null) continue;
                    try
                    {
                        object? registvalue = sk.GetValue("DisplayName");
                        if (registvalue != null)
                        {
                            string? value = Convert.ToString(registvalue);
                            if (string.IsNullOrEmpty(value)) continue;
                            installed.Add(new InstalledProgram
                            {
                                DisplayName = Convert.ToString(sk.GetValue("DisplayName")),
                                Version = Convert.ToString(sk.GetValue("DisplayVersion")),
                                InstalledDate = Convert.ToString(sk.GetValue("InstallDate")),
                                Publisher = Convert.ToString(sk.GetValue("Publisher")),
                                UnninstallCommand = Convert.ToString(sk.GetValue("UninstallString")),
                                ModifyPath = Convert.ToString(sk.GetValue("ModifyPath"))
                            });
                        };
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return installed;
        }
    }
}
