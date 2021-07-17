using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Team
    {
        private const char SEPARATOR = '|';

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("alternate_name")]
        public object AlternateName { get; set; }

        [JsonProperty("fifa_code")]
        public string FifaCode { get; set; }

        [JsonProperty("code")]
        private string FifaCode2 { set { FifaCode = value; } }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("group_letter")]
        public string GroupLetter { get; set; }

        [JsonProperty("wins")]
        public long Wins { get; set; }

        [JsonProperty("draws")]
        public long Draws { get; set; }

        [JsonProperty("losses")]
        public long Losses { get; set; }

        [JsonProperty("games_played")]
        public long GamesPlayed { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }

        [JsonProperty("goals_for")]
        public long GoalsFor { get; set; }

        [JsonProperty("goals")]
        public long GoalsFor1 { set { GoalsFor = value; } }

        [JsonProperty("goals_against")]
        public long GoalsAgainst { get; set; }

        [JsonProperty("goal_differential")]
        public long GoalDifferential { get; set; }

        public override string ToString() => $"{Country} ({FifaCode})";

        public string FormatForFile() => $"{Id}{SEPARATOR}{Country}{SEPARATOR}{FifaCode}{SEPARATOR}{GroupId}{SEPARATOR}{GroupLetter}{SEPARATOR}{Wins}{SEPARATOR}{Draws}{SEPARATOR}{Losses}{SEPARATOR}{GamesPlayed}{SEPARATOR}{Points}{SEPARATOR}{GoalsFor}{SEPARATOR}{GoalsAgainst}{SEPARATOR}{GoalDifferential}";

        public static Team ParseFromFile(string str)
        {
            string[] attributes = str.Split(SEPARATOR);

            return new Team
            {
                Id = int.Parse(attributes[0]),
                Country = attributes[1],
                FifaCode = attributes[2],
                GroupId = int.Parse(attributes[3]),
                GroupLetter = attributes[4],
                Wins = int.Parse(attributes[5]),
                Draws = int.Parse(attributes[6]),
                Losses = int.Parse(attributes[7]),
                GamesPlayed = int.Parse(attributes[8]),
                Points = int.Parse(attributes[9]),
                GoalsFor = int.Parse(attributes[10]),
                GoalsAgainst = int.Parse(attributes[11]),
                GoalDifferential = int.Parse(attributes[12]),
                AlternateName = null
            };
        }

    }
}
