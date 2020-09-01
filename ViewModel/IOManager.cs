using System;
using System.Configuration;
using System.Windows;

namespace ImageSorter2._0.ViewModel
{
    public class IOManager
    {
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while saving the settings.\n" + e.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] ?? "";
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the settings.\n" + e.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return "";
        }
    }
}