using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface IFootballRepository
    {
        Task<List<Team>> GetTeamsAsync();
        Task<List<Match>> GetMatchesAsync();
        Task<List<Match>> GetMatchesAsync(Team team);
        Task<List<Player>> GetPlayersAsync(Team team);
    }
}
