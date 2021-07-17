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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for PlayerUserControl.xaml
    /// </summary>
    public partial class PlayerUserControl : UserControl
    {
        public Player Player { get; set; }
        public Match Match { get; set; }
        private string PICTURE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\FootballApp\\shirt.png";
        public PlayerUserControl(Player player, Match match)
        {
            InitializeComponent();
            Match = match;
            Player = player;
            SetData(player);
        }

        private void SetData(Player player)
        {
            lbName.Content = $"{player.Name} ({player.ShirtNumber})";
            imgPlayer.Source = new BitmapImage(new Uri(player.PicturePath ?? PICTURE_PATH));
        }
    }
}
