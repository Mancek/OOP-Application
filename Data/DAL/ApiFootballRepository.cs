using Data.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    class ApiFootballRepository : IFootballRepository
    {
        private const string MEN_URL = "https://world-cup-json-2018.herokuapp.com";
        private const string WOMEN_URL = "http://worldcup.sfg.io";

        private string apiUrl;

        private readonly ISettingsRepository settingsRepository;

        public ApiFootballRepository()
        {
            settingsRepository = RepositoryFactory.GetSettingsRepository();
            if (settingsRepository.DoSettingsExist())
            {
                apiUrl = settingsRepository.GetSettings().Type == ChampionshipType.Men ? MEN_URL : WOMEN_URL;
            }

        }

        public async Task<List<Team>> GetTeamsAsync()
            => await GetParsedDataAsync<List<Team>>($"{apiUrl}/teams/results");

        public async Task<List<Match>> GetMatchesAsync()
        {
            string fifaCode = settingsRepository.GetUserData().FavoriteTeam.FifaCode;

            return await GetParsedDataAsync<List<Match>>($"{apiUrl}/matches/country?fifa_code={fifaCode}");
        }

        public async Task<List<Match>> GetMatchesAsync(Team team)
        {
            string fifaCode = team.FifaCode;

            return await GetParsedDataAsync<List<Match>>($"{apiUrl}/matches/country?fifa_code={fifaCode}");
        }

        public async Task<List<Player>> GetPlayersAsync(Team team)
        {
            if (string.IsNullOrEmpty(apiUrl))
            {
                apiUrl = settingsRepository.GetSettings().Type == ChampionshipType.Men ? MEN_URL : WOMEN_URL;
            }
            string fifaCode = team.FifaCode;

            List<Match> matches = await GetParsedDataAsync<List<Match>>($"{apiUrl}/matches/country?fifa_code={fifaCode}");
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

        private static Task<IRestResponse<T>> GetDataAsync<T>(string urlString)
            => new RestClient(urlString).ExecuteAsync<T>(new RestRequest());

        private static IRestResponse<T> GetData<T>(string urlString)
            => new RestClient(urlString).Execute<T>(new RestRequest());

        private static T DeserializeData<T>(IRestResponse<T> data)
            => JsonConvert.DeserializeObject<T>(data.Content);

        private async static Task<T> GetParsedDataAsync<T>(string urlString)
        {
            IRestResponse<T> restResponse = await GetDataAsync<T>(urlString);
            return DeserializeData<T>(restResponse);
        }
    }
}
