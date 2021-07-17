using Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public static class RepositoryFactory
    {
        public static IFootballRepository GetFootballRepository()
        {
            bool useWeb = int.Parse(File.ReadAllText(FileSettingsRepository.SETTINGS_DIRECTORY + "\\jsonSettings.txt")) == (int)ApiGetDataMethod.API;
            if (useWeb)
            { 
                return new ApiFootballRepository(); 
            }
            else 
            { 
                return new FileFootballRepository(); 
            }
            //=> int.Parse(File.ReadAllText(FileSettingsRepository.SETTINGS_DIRECTORY + "\\jsonSettings.txt")) == (int)ApiGetDataMethod.API ?
            //    new ApiFootballRepository() : new FileFootballRepository();
        }

        public static ISettingsRepository GetSettingsRepository()
            => new FileSettingsRepository();
    }
}
