using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Player
    {
        private const char SEPARATOR = '|';

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("captain")]
        public bool Captain { get; set; }

        [JsonProperty("shirt_number")]
        public long ShirtNumber { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        public string PicturePath { get; set; }

        public int Goals { get; set; } = 0;
        public int YellowCards { get; set; } = 0;

        public string FormatForFile() => $"{Name}{SEPARATOR}{Captain}{SEPARATOR}{ShirtNumber}{SEPARATOR}{Position}{SEPARATOR}{PicturePath}";

        public static Player ParseFromFile(string str)
        {
            string[] attributes = str.Split(SEPARATOR);

            return new Player
            {
                Name = attributes[0],
                Captain = bool.Parse(attributes[1]),
                ShirtNumber = long.Parse(attributes[2]),
                Position = attributes[3],
                PicturePath = attributes[4]
            };
        }

        public override bool Equals(object obj) => obj is Player player && Name == player.Name;

        public override int GetHashCode() => 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);

    }
}
