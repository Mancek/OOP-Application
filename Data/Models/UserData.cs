using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UserData
    {
        private const char SEPARATOR = '|';

        public Team FavoriteTeam { get; set; }
        public List<Player> FavoritePlayers { get; set; }
        public List<Player> AllPlayers { get; set; }

        public string FormatForFile()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(FavoriteTeam.FormatForFile()).Append(SEPARATOR);
            FavoritePlayers.ForEach(p => sb.Append(p.FormatForFile()).Append(SEPARATOR));
            AllPlayers.ForEach(p => sb.Append(p.FormatForFile()).Append(SEPARATOR));

            return sb.ToString();
        }

        public static UserData ParseFromFile(string str)
        {
            string[] attributes = str.Split(SEPARATOR);

            List<Player> favoritePlayers = new List<Player>();
            List<Player> allPlayers = new List<Player>();
            for (int i = 13; i < 13 + 3 * 5; i += 5)
            { 
                favoritePlayers.Add(Player.ParseFromFile(string.Join("|", new string[] { attributes[i], attributes[i + 1], attributes[i + 2], attributes[i + 3], attributes[i + 4] })));
            }
            for (int i = 28; i < attributes.Count()-5; i += 5)
            {
                allPlayers.Add(Player.ParseFromFile(string.Join("|", new string[] { attributes[i], attributes[i + 1], attributes[i + 2], attributes[i + 3], attributes[i + 4] })));
            }

            return new UserData
            {
                FavoriteTeam = Team.ParseFromFile(string.Join("|", new string[] {
                    attributes[0], attributes[1], attributes[2], attributes[3],
                    attributes[4], attributes[5], attributes[6], attributes[7],
                    attributes[8], attributes[9], attributes[10], attributes[11], attributes[12] })),

                FavoritePlayers = favoritePlayers,
                AllPlayers = allPlayers
            };
        }
    }
}
