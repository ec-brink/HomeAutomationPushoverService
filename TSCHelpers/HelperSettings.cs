using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace TSCHelpers
{
    public static class HelperSettings
    {
        public static ServiceHelperSettings Settings { get; internal set; }

        public static void ReadSettings()
        {
            try
            {
                string settingsJson = File.ReadAllText("settings.json");
                Settings = JsonConvert.DeserializeObject<ServiceHelperSettings>(settingsJson);
            }
            catch (FileNotFoundException ex)
            {
                LogHelper.LogError("Error loading helper settings: File not found", ex);
                throw;
            }
            catch (JsonException ex)
            {
                LogHelper.LogError("Error loading helper settings: Invalid JSON format", ex);
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Unexpected error loading helper settings", ex);
                throw;
            }
        }
    }

    public class ServiceHelperSettings
    {
        // Windows Event Log Application Name
        public string WindowsEventLogApplicationName { get; set; }

        // Logging Settings
        public string LogFileLevel { get; set; } = "Information";
        public string LogDirectory { get; set; } = "logs";

        // Database Settings
        public string DatabaseConnectionString { get; set; }
    }
}
