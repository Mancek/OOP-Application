using Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    class FileFootballRepository : IFootballRepository
    {
        private static string API_DIRECTORY = $"{FileSettingsRepository.SETTINGS_DIRECTORY}\\worldcup.sfg.io";
        private readonly string MEN_DIRECTORY = $"{API_DIRECTORY}\\men";
        private readonly string WOMEN_DIRECTORY = $"{API_DIRECTORY}\\women";

        private readonly string apiDirectory;

        private readonly ISettingsRepository settingsRepository;

        public FileFootballRepository()
        {
            settingsRepository = RepositoryFactory.GetSettingsRepository();
            if (settingsRepository.DoSettingsExist())
            {
                apiDirectory = settingsRepository.GetSettings().Type == ChampionshipType.Men ? MEN_DIRECTORY : WOMEN_DIRECTORY;
            }

        }

        public async Task<List<Team>> GetTeamsAsync()
            => await GetParsedDataAsync<List<Team>>($"{apiDirectory}\\teams");

        public async Task<List<Match>> GetMatchesAsync()
        {
            string fifaCode = settingsRepository.GetUserData().FavoriteTeam.FifaCode;

            List<Match> matches = await GetParsedDataAsync<List<Match>>($"{apiDirectory}\\matches");
            return (List<Match>)matches.Where(m => m.HomeTeam.FifaCode == fifaCode || m.AwayTeam.FifaCode == fifaCode);
        }

        public async Task<List<Match>> GetMatchesAsync(Team team)
        {
            string fifaCode = team.FifaCode;

            List<Match> matches = await GetParsedDataAsync<List<Match>>($"{apiDirectory}\\matches");
            return (List<Match>)matches.Where(m => m.HomeTeam.FifaCode == fifaCode || m.AwayTeam.FifaCode == fifaCode);
        }

        public async Task<List<Player>> GetPlayersAsync(Team team)
        {
            string fifaCode = team.FifaCode;

            List<Match> matches = await GetParsedDataAsync<List<Match>>($"{apiDirectory}/matches/country?fifa_code={fifaCode}");
            List<Player> players;
            if (matches[0].HomeTeam.Country == team.Country)
            {
                players = matches[0].HomeTeamStatistics.StartingEleven;
                players.AddRange(matches[0].HomeTeamStatistics.Substitutes);
            }
            else
            {
                players = matches[0].AwayTeamStatistics.StartingEleven;
                players.AddRange(matches[0].AwayTeamStatistics.Substitutes);
            }

            return players;
        }

        private static T DeserializeData<T>(string data)
            => JsonConvert.DeserializeObject<T>(data);
        private async static Task<T> GetParsedDataAsync<T>(string urlString)
        {
            byte[] result;
            using (FileStream SourceStream = File.Open(urlString, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }
            string data = Encoding.ASCII.GetString(result);
            return DeserializeData<T>(data);
        }
    }
}
