using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core
{
    internal class RegistryFinder
    {
        public static string Find(RegistryKey registryKey, string subKeyPath, string with)
        {
            string value;
            RegistryKey subkey;
            RegistryKey subkeyTarget;
            RegistryKey key = registryKey.OpenSubKey(subKeyPath);
            foreach (string subKey in key.GetSubKeyNames())
            {
                subkey = key.OpenSubKey(subKey);
                value = (string)subkey.GetValue("");
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Contains(with))
                    {
                        foreach (string targetSubkey in subkey.GetSubKeyNames())
                        {
                            if (targetSubkey == "ProgID")
                            {
                                subkeyTarget = subkey.OpenSubKey(targetSubkey);
                                value = (string)subkeyTarget.GetValue("");
                                subkeyTarget.Close();

                                subkeyTarget = registryKey.OpenSubKey(value);
                                subkeyTarget = subkeyTarget.OpenSubKey(@"shell\edit\command");
                                value = (string)subkeyTarget.GetValue("");
                                subkeyTarget.Close();
                                subkey.Close();
                                key.Close();
                                return value;
                            }
                        }
                    }
                }
                subkey.Close();
            }
            key.Close();
            return "";
        }

    }
}
