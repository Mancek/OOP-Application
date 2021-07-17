using Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Data.DAL
{
    class FileSettingsRepository : ISettingsRepository
    {
        public static string SETTINGS_DIRECTORY = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\FootballApp";
        private readonly string SETTINGS_FILEPATH = $"{SETTINGS_DIRECTORY}\\settings.txt";
        private readonly string USERDATA_FILEPATH = $"{SETTINGS_DIRECTORY}\\userdata.txt";
        private readonly string SCREEN_FILEPATH = $"{SETTINGS_DIRECTORY}\\screen.txt";
        private readonly string PICTURES_FILEPATH = $"{SETTINGS_DIRECTORY}\\picturepaths.txt";
        private readonly string JSON_SETTINGS_FILEPATH = $"{SETTINGS_DIRECTORY}\\jsonSettings.txt";

        public FileSettingsRepository()
        {
            if(!Directory.Exists(SETTINGS_DIRECTORY))
            {
                Directory.CreateDirectory(SETTINGS_DIRECTORY);
            }
            if (!File.Exists(JSON_SETTINGS_FILEPATH))
            {
                File.WriteAllLines(JSON_SETTINGS_FILEPATH, new string[] { ((int)ApiGetDataMethod.API).ToString() });
            }
        }

        public bool DoSettingsExist()
        {
            if (!File.Exists(SETTINGS_FILEPATH))
            {
                return false;
            }
            return true;
        }

        public bool DoesUserDataExist()
        {
            if (!File.Exists(USERDATA_FILEPATH))
            {
                return false;
            }
            return true;
        }

        public bool DoesScreenSizeExist()
        {
            if (!File.Exists(SCREEN_FILEPATH))
            {
                return false;
            }
            return true;
        }
        public void SaveSettings(Settings data)
        {
            File.WriteAllLines(SETTINGS_FILEPATH, new string[] { data.FormatForFile() });
        }

        public void SaveUserData(UserData data)
        {
            File.WriteAllLines(USERDATA_FILEPATH, new string[] { data.FormatForFile() });
        }
        public void SaveScreenSize(Screen data)
        {
            File.WriteAllLines(SCREEN_FILEPATH, new string[] { data.FormatForFile() });
        }

        public void DeleteUserData()
        {
            if(File.Exists(USERDATA_FILEPATH))
            {
                File.Delete(USERDATA_FILEPATH);
            }
        }

        public Settings GetSettings()
        {
            try
            {
                string stringData = File.ReadAllText(SETTINGS_FILEPATH);
                return Settings.ParseFromFile(stringData);
            }
            catch (Exception)
            {
                return new Settings { Language = "en", Type = ChampionshipType.Men };
            }

        }
        public UserData GetUserData()
        {
            string stringData = File.ReadAllText(USERDATA_FILEPATH);
            return UserData.ParseFromFile(stringData);
        }

        public Screen GetScreenSize()
        {
            string stringData = File.ReadAllText(SCREEN_FILEPATH);
            return Screen.ParseFromFile(stringData);
        }
    }

}
