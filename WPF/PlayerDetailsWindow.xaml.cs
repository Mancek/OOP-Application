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
    /// Interaction logic for PlayerDetailsWindow.xaml
    /// </summary>
    public partial class PlayerDetailsWindow : Window
    {
        public Player Player { get; set; }
        public Match Match { get; set; }
        private string PICTURE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\FootballApp\\shirt.png";
        public PlayerDetailsWindow(Player player, Match match)
        {
            InitializeComponent();
            Match = match;
            Player = player;
            SetData(player);
        }

        private void SetData(Player player)
        {
            lbName.Content = player.Name;
            lbNumber.Content = player.ShirtNumber;
            lbPosition.Content = player.Position;
            lbPlayer.Content = player.Captain ? "Captain" : "Player";
            lbGoals.Content = $"{Properties.Resources.goalsText}: {Match.HomeTeamEvents.Where(e => e.Player == player.Name && e.TypeOfEvent == TypeOfEvent.Goal).Count() + Match.AwayTeamEvents.Where(e => e.Player == player.Name && e.TypeOfEvent == TypeOfEvent.Goal).Count()}";
            lbYellowCards.Content = $"{Properties.Resources.yellowCardText}: {Match.HomeTeamEvents.Where(e => e.Player == player.Name && (e.TypeOfEvent == TypeOfEvent.YellowCard || e.TypeOfEvent == TypeOfEvent.YellowCardSecond)).Count() + Match.AwayTeamEvents.Where(e => e.Player == player.Name && (e.TypeOfEvent == TypeOfEvent.YellowCard || e.TypeOfEvent == TypeOfEvent.YellowCardSecond)).Count() }";
            imgPlayer.Source = new BitmapImage(new Uri(player.PicturePath ?? PICTURE_PATH));
        }
    }
}
