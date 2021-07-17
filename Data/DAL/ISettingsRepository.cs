using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface ISettingsRepository
    {
        bool DoSettingsExist();
        bool DoesUserDataExist();
        bool DoesScreenSizeExist();
        void SaveSettings(Settings data);
        void SaveUserData(UserData data);
        void SaveScreenSize(Screen data);
        void DeleteUserData();
        Settings GetSettings();
        UserData GetUserData();
        Screen GetScreenSize();
    }
}
