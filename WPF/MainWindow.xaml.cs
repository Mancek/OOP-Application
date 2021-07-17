using Data.DAL;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string PICTURE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\FootballApp\\shirt1.png";
        private readonly ISettingsRepository settingsRepository;
        private readonly IFootballRepository footballRepository;

        private Team chosenFirstTeam;
        private Team chosenSecondTeam;
        private Team favoriteTeam;
        private List<Match> allMatches;
        private List<Team> allTeams;
        private Match selectedMatch;
        public MainWindow()
        {
            settingsRepository = RepositoryFactory.GetSettingsRepository();
            SetCulture();
            InitializeComponent();
            footballRepository = RepositoryFactory.GetFootballRepository();
            CheckForSettings();
            SetScreenSize();
        }

        private void SetScreenSize()
        {
            if (settingsRepository.DoesScreenSizeExist())
            {
                switch (settingsRepository.GetScreenSize().Type)
                {
                    case ScreenSizeType.FullScreen:
                        WindowState = WindowState.Maximized;
                        break;
                    case ScreenSizeType.Size480p:
                        Height = 480;
                        Width = 720;
                        break;
                    case ScreenSizeType.Size576p:
                        Height = 576;
                        Width = 720;
                        break;
                    case ScreenSizeType.Size720p:
                        Height = 720;
                        Width = 1280;
                        break;
                    case ScreenSizeType.Size1080p:
                        Height = 1080;
                        Width = 1920;
                        break;
                    default:
                        WindowState = WindowState.Maximized;
                        break;
                }
            }
        }

        private void SetCulture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(settingsRepository.GetSettings().Language ?? "en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(settingsRepository.GetSettings().Language ?? "en");
        }

        private void CheckForSettings()
        {
            if (!settingsRepository.DoSettingsExist() || !settingsRepository.DoesScreenSizeExist())
            {
                if (new SettingsWindow().ShowDialog() == true)
                {
                    Application.Current.Shutdown();
                }
            }
            PrepareData();
        }

        private async void PrepareData()
        {
            Application.Current.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait);
            favoriteTeam = settingsRepository.GetUserData().FavoriteTeam;
            allTeams = await footballRepository.GetTeamsAsync();
            cbFirstTeam.ItemsSource = allTeams;
            cbFirstTeam.SelectedItem = allTeams.Where(t => t.FifaCode == favoriteTeam.FifaCode).FirstOrDefault();
            chosenFirstTeam = cbFirstTeam.SelectedItem as Team;
            FillSecondComboBox();
        }

        private async void FillSecondComboBox()
        {
            Application.Current.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait);
            allMatches = await footballRepository.GetMatchesAsync(chosenFirstTeam);
            List<Team> teams = allMatches.Select(m => m.HomeTeam).Where(m => m.FifaCode != chosenFirstTeam.FifaCode).ToList();
            teams.AddRange(allMatches.Select(m => m.AwayTeam).Where(m => m.FifaCode != chosenFirstTeam.FifaCode));

            cbSecondTeam.Items.Clear();
            teams.ForEach(t => cbSecondTeam.Items.Add(t));
            cbSecondTeam.SelectedIndex = 0;
            chosenSecondTeam = cbSecondTeam.SelectedItem as Team;
            SetScore(chosenFirstTeam, chosenSecondTeam, allMatches);
            FieldSetup();
        }

        private void FieldSetup()
        {
            List<Player> firstTeam = selectedMatch.HomeTeamStatistics.StartingEleven;
            List<Player> secondTeam = selectedMatch.AwayTeamStatistics.StartingEleven;
            if (selectedMatch.HomeTeam.FifaCode == favoriteTeam.FifaCode)
            {
                List<Player> players = settingsRepository.GetUserData().AllPlayers;
                firstTeam.ForEach(p => players.ForEach(p1 =>
                {
                    if(p.Name == p1.Name)
                    {
                        p.PicturePath = p1.PicturePath;
                    }
                }));
                secondTeam.ForEach(p => p.PicturePath = PICTURE_PATH);
            }
            else if(selectedMatch.AwayTeam.FifaCode == favoriteTeam.FifaCode)
            {
                List<Player> players = settingsRepository.GetUserData().AllPlayers;
                secondTeam.ForEach(p => players.ForEach(p1 =>
                {
                    if (p.Name == p1.Name)
                    {
                        p.PicturePath = p1.PicturePath;
                    }
                }));
                firstTeam.ForEach(p => p.PicturePath = PICTURE_PATH);
            }
            else
            {
                firstTeam.ForEach(p => p.PicturePath = PICTURE_PATH);
            }
            IEnumerable<StackPanel> stackPanels = fieldGrid.Children.Cast<StackPanel>();
            stackPanels.ToList().ForEach(p => p.Children.Clear());

            SetupTeam(firstTeam, new int[] { 0, 1, 3, 4 });
            SetupTeam(secondTeam, new int[] { 9, 8, 6, 5 });
        }

        private void SetupTeam(List<Player> firstTeam, int[] pos)
        {
            PlayerUserControl control;

            Player goalie = firstTeam.Where(p => p.Position == Position.Goalie.ToString()).FirstOrDefault();
            IEnumerable<Player> defenders = firstTeam.Where(p => p.Position == Position.Defender.ToString());
            IEnumerable<Player> midfield = firstTeam.Where(p => p.Position == Position.Midfield.ToString());
            IEnumerable<Player> forward = firstTeam.Where(p => p.Position == Position.Forward.ToString());

            control = new PlayerUserControl(goalie, selectedMatch);
            control.MouseDoubleClick += Control_MouseDoubleClick;
            StackPanel stackPanel = fieldGrid.Children.Cast<StackPanel>().Where(i => Grid.GetColumn(i) == pos[0]).FirstOrDefault();
            stackPanel.Children.Add(control);

            defenders.ToList().ForEach(p =>
            {
                control = new PlayerUserControl(p, selectedMatch);
                control.MouseDoubleClick += Control_MouseDoubleClick;
                stackPanel = fieldGrid.Children.Cast<StackPanel>().Where(i => Grid.GetColumn(i) == pos[1]).FirstOrDefault();
                stackPanel.Children.Add(control);
            });

            midfield.ToList().ForEach(p =>
            {
                control = new PlayerUserControl(p, selectedMatch);
                control.MouseDoubleClick += Control_MouseDoubleClick;
                stackPanel = fieldGrid.Children.Cast<StackPanel>().Where(i => Grid.GetColumn(i) == pos[2]).FirstOrDefault();
                stackPanel.Children.Add(control);
            });

            forward.ToList().ForEach(p =>
            {
                control = new PlayerUserControl(p, selectedMatch);
                control.MouseDoubleClick += Control_MouseDoubleClick;
                stackPanel = fieldGrid.Children.Cast<StackPanel>().Where(i => Grid.GetColumn(i) == pos[3]).FirstOrDefault();
                stackPanel.Children.Add(control);
            });
        }

        private void Control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlayerUserControl panel = sender as PlayerUserControl;
            new PlayerDetailsWindow(panel.Player, panel.Match).ShowDialog();
        }

        private void SetScore(Team chosenFirstTeam, Team chosenSecondTeam, List<Match> matches)
        {
            selectedMatch = allMatches.Where(m =>
                (m.HomeTeam.FifaCode == chosenFirstTeam.FifaCode && m.AwayTeam.FifaCode == chosenSecondTeam.FifaCode) ||
                (m.HomeTeam.FifaCode == chosenSecondTeam.FifaCode && m.AwayTeam.FifaCode == chosenFirstTeam.FifaCode)).FirstOrDefault();

            lbScore.Content = selectedMatch.HomeTeam.FifaCode == chosenFirstTeam.FifaCode ?
                $"{selectedMatch.HomeTeam.GoalsFor}:{selectedMatch.AwayTeam.GoalsFor}" :
                $"{selectedMatch.AwayTeam.GoalsFor}:{selectedMatch.HomeTeam.GoalsFor}";

            Application.Current.Dispatcher.Invoke(() => Mouse.OverrideCursor = null);
        }


        private void cbFirstTeam_DropDownClosed(object sender, EventArgs e)
        {
            Team oldTeam = chosenFirstTeam;
            chosenFirstTeam = cbFirstTeam.SelectedItem as Team;
            if (oldTeam == chosenFirstTeam)
            {
                return;
            }
            FillSecondComboBox();
        }

        private void cbSecondTeam_DropDownClosed(object sender, EventArgs e)
        {
            chosenFirstTeam = cbFirstTeam.SelectedItem as Team;
            chosenSecondTeam = cbSecondTeam.SelectedItem as Team;
            SetScore(chosenFirstTeam, chosenSecondTeam, allMatches);
            FieldSetup();
        }

        private void Firstbtn_Animation_Completed(object sender, EventArgs e)
        {
            new TeamDetailsWindow(chosenFirstTeam).ShowDialog();
        }

        private void Secondbtn_Animation_Completed(object sender, EventArgs e)
        {
            new TeamDetailsWindow(chosenSecondTeam).ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.appClosing, "Close Application", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void ctxItemSettings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
            SetScreenSize();
        }
    }
}
