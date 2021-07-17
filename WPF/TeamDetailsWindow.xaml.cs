using Data.DAL;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for TeamDetailsWindow.xaml
    /// </summary>
    public partial class TeamDetailsWindow : Window
    {
        private IFootballRepository footballRepository;
        public TeamDetailsWindow(Team team)
        {
            InitializeComponent();
            footballRepository = RepositoryFactory.GetFootballRepository();
            FillData(team);
        }

        private async void FillData(Team team)
        {
            Application.Current.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait);

            List<Match> matches = await footballRepository.GetMatchesAsync(team);
            lbName.Content = team.Country;
            lbFifaCode.Content = team.FifaCode;
            lbPlayed.Content = $"{Properties.Resources.playedText}: { matches.Count() }";
            lbWon.Content = $"{Properties.Resources.wonText}: { matches.Where(m => (m.HomeTeam.FifaCode == team.FifaCode && m.HomeTeam.GoalsFor > m.AwayTeam.GoalsFor) || (m.AwayTeam.FifaCode == team.FifaCode && m.AwayTeam.GoalsFor > m.HomeTeam.GoalsFor)).Count() }";
            lbLost.Content = $"{Properties.Resources.lostText}: { matches.Where(m => (m.HomeTeam.FifaCode == team.FifaCode && m.HomeTeam.GoalsFor < m.AwayTeam.GoalsFor) || (m.AwayTeam.FifaCode == team.FifaCode && m.AwayTeam.GoalsFor < m.HomeTeam.GoalsFor)).Count() }";
            lbUndecided.Content = $"{Properties.Resources.undecidedText}: { matches.Where(m => m.HomeTeam.GoalsFor == m.AwayTeam.GoalsFor).Count() }";

            Application.Current.Dispatcher.Invoke(() => Mouse.OverrideCursor = null);
        }
    }
}
