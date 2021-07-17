using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Settings
    {
        private const char SEPARATOR = '|';

        public string Language { get; set; }
        public ChampionshipType Type { get; set; }

        public string FormatForFile() => $"{Language}{SEPARATOR}{(int)Type}";

        public static Settings ParseFromFile(string str)
        {
            string[] attributes = str.Split(SEPARATOR);

            return new Settings
            {
                Language = attributes[0],
                Type = (ChampionshipType)Enum.Parse(typeof(ChampionshipType), attributes[1])
            };
        }
    }
}
