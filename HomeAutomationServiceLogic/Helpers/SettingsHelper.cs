using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using TSCHelpers;

using Newtonsoft.Json;

namespace HomeAutomationServiceLogic.Helpers
{
    public static class SettingsHelper
    {
        public static ServiceSettings Settings { get; internal set; }

        public static void ReadSettings()
        {
            HelperSettings.ReadSettings();

            try
            {
                string settingsJson = File.ReadAllText("settings.json");
                Settings = JsonConvert.DeserializeObject<ServiceSettings>(settingsJson);
            }
            catch (FileNotFoundException ex)
            {
                LogHelper.LogError("Error loading settings: File not found", ex);
                throw;
            }
            catch (JsonException ex)
            {
                LogHelper.LogError("Error loading settings: Invalid JSON format", ex);
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Unexpected error loading settings", ex);
                throw;
            }
        }
    }

    public class ServiceSettings : ServiceHelperSettings
    {
        // MQTT Settings
        public string MqttServer { get; set; }
        public int? MqttPort { get; set; }
        public string MqttUsername { get; set; }
        public string MqttPassword { get; set; }

        // SP Settings
        public string StoredProcedure { get; set; }

        // Sleep between processing messages to database
        public int DatabaseWriteThreadSleepMilliSeconds = 100;

        // Sleep between processing messages to database
        public int ServiceStopWaitSeconds = 5;
    }
}
