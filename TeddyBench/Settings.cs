using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeddyBench
{
    public class Settings
    {
        public string Username = "";
        public bool NfcEnabled = false;
        public bool DebugWindow = false;

        public static Settings FromFile(string file)
        {
            Settings s = null;
            if (File.Exists(file))
            {
                try
                {
                    s = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(file));
                }
                catch (Exception ex)
                {
                    LogWindow.Log(LogWindow.eLogLevel.Information, ex.Message);
                }

            }
            if (s != null)
            {
                return s;
            }

            return new Settings();
        }

        public bool Save(string file)
        {
            try
            {
                File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                LogWindow.Log(LogWindow.eLogLevel.Information, ex.Message);
            }
            return false;
        }
    }
}
