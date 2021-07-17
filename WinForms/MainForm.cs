using Data.DAL;
using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Models;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private OpenFileDialog ofd = new OpenFileDialog();
        ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
        private readonly string PICTURE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\FootballApp\\shirt.png";
        private readonly ISettingsRepository settingsRepository;
        private readonly IFootballRepository footballRepository;

        private Team choosenTeam;
        private HashSet<Player> allPlayers = new HashSet<Player>();
        private HashSet<Player> favoritePlayers = new HashSet<Player>();
        private List<Player> sortedByGoals;
        private List<Player> sortedByYellowCards;
        private List<Match> sortedByVisitors;

        private HashSet<PlayerUserControl> selectedPanels = new HashSet<PlayerUserControl>();
        private HashSet<Player> selectedPlayers = new HashSet<Player>();


        public MainForm()
        {
            settingsRepository = RepositoryFactory.GetSettingsRepository();
            footballRepository = RepositoryFactory.GetFootballRepository(); 
            SetCulture();
            InitializeComponent();
            InitDnD();
            InitOpenFileDialog();
        }

        private void SetCulture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(settingsRepository.GetSettings().Language ?? "en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(settingsRepository.GetSettings().Language ?? "en");
        }

        private void InitOpenFileDialog()
        {
            ofd.Filter = "Picture|*.jpeg;*.jpg;*.png;|All files|*.*";
            ofd.Multiselect = false;
            ofd.Title = "Load picture...";
            ofd.InitialDirectory = Application.StartupPath;
        }

        private void InitDnD()
        {
            pnlFavoritePlayers.AllowDrop = true;
            pnlFavoritePlayers.DragEnter += PnlFavoritePlayers_DragEnter;
            pnlFavoritePlayers.DragDrop += PnlFavoritePlayers_DragDrop;
        }

        private void PnlFavoritePlayers_DragDrop(object sender, DragEventArgs e)
        {
            PlayerUserControl pnl = (PlayerUserControl)e.Data.GetData(typeof(PlayerUserControl));
            if(favoritePlayers.Count > 2)
            {
                MessageBox.Show(resources.GetString("msgBoxAdding"), "Application", MessageBoxButtons.OK);
                return;
            }
            if (selectedPlayers.Count == 1)
            {
                favoritePlayers.Add(allPlayers.FirstOrDefault(p => p.Name == pnl.PlayerName));
                ClearSelection();
                ShowFavoritePlayersData();
            }
            else
            {
                AddtoFavorite();
            }

        }

        private void PnlFavoritePlayers_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.Copy;

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForSettings();
        }

        private void CheckForSettings()
        {
            if (!settingsRepository.DoSettingsExist())
            {
                if (new SettingsForm().ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            if (!settingsRepository.DoesUserDataExist())
            {
                if (new TeamForm().ShowDialog(this) != DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    LoadPlayersAsync();
                }
            }
            else
            {
                PrepareData();
            }

        }

        private void PrepareData()
        {
            choosenTeam = settingsRepository.GetUserData().FavoriteTeam;
            allPlayers = new HashSet<Player>(settingsRepository.GetUserData().AllPlayers);
            favoritePlayers = new HashSet<Player>(settingsRepository.GetUserData().FavoritePlayers);

            ShowFavoritePlayersData();
            ShowAllPlayersData();
            SortLists();
        }

        public void SetTeam(Team team)
        {
            choosenTeam = team;
        }

        private async void LoadPlayersAsync()
        {
            pnlAllPlayers.Controls.Clear();
            allPlayers = new HashSet<Player>(await footballRepository.GetPlayersAsync(choosenTeam));
            ShowAllPlayersData();
            SortLists();
        }

        private void SaveUserData()
        {
            settingsRepository.SaveUserData(new UserData
            {
                FavoriteTeam = choosenTeam,
                FavoritePlayers = new List<Player>(favoritePlayers),
                AllPlayers = new List<Player>(allPlayers)
            });
        }

        private void addToFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedPlayers.Count < 3)
            {
                MessageBox.Show(resources.GetString("msgBoxToolStrip"), "Application", MessageBoxButtons.OK);
            }
            else
            {
                AddtoFavorite();
            }
        }

        private void AddtoFavorite()
        {
            favoritePlayers?.Clear();
            favoritePlayers = new HashSet<Player>(selectedPlayers.Take(3));

            ClearSelection();
            ShowFavoritePlayersData();
        }

        private void ClearSelection()
        {
            selectedPanels.ToList().ForEach(p => p.BackColor = Color.Transparent);
            selectedPanels.Clear();
            selectedPlayers.Clear();
        }

        private void ShowFavoritePlayersData()
        {
            pnlFavoritePlayers.Controls.Clear();
            favoritePlayers.ToList().ForEach(p =>
            {
                PlayerUserControl pnl = new PlayerUserControl
                {
                    PlayerName = p.Name,
                    PlayerNumber = p.ShirtNumber.ToString(),
                    PlayerPosition = p.Position,
                    PlayerCaptain = p.Captain ? "Captain" : "Player",
                };
                if (p.PicturePath != null)
                {
                    pnl.pictureBox1.Image = Image.FromFile(p.PicturePath);
                }
                else
                {
                    pnl.pictureBox1.Image = Image.FromFile(PICTURE_PATH);
                }
                pnl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pnl.ContextMenuStrip = cmsFavorites;
                pnlFavoritePlayers.Controls.Add(pnl);
            });
        }

        private void ShowAllPlayersData()
        {
            pnlAllPlayers.Controls.Clear();
            allPlayers.ToList().ForEach(p =>
            {
                PlayerUserControl pnl = new PlayerUserControl
                {
                    PlayerName = p.Name,
                    PlayerNumber = p.ShirtNumber.ToString(),
                    PlayerPosition = p.Position,
                    PlayerCaptain = p.Captain ? "Captain" : "Player",

                };
                if (p.PicturePath != null)
                {
                    pnl.pictureBox1.Image = Image.FromFile(p.PicturePath);
                }
                else
                {
                    pnl.pictureBox1.Image = Image.FromFile(PICTURE_PATH);
                    p.PicturePath = PICTURE_PATH;
                }
                pnl.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pnl.ContextMenuStrip = cmsPlayers;
                pnl.MouseDown += Pnl_MouseDown;
                pnlAllPlayers.Controls.Add(pnl);
            });
        }

        private void Pnl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PlayerUserControl choosenPanel = sender as PlayerUserControl;

                if(!selectedPlayers.Add(allPlayers.FirstOrDefault(p => p.Name == choosenPanel.PlayerName)))
                {
                    choosenPanel.BackColor = Color.Transparent;
                    selectedPlayers.Remove(allPlayers.FirstOrDefault(p => p.Name == choosenPanel.PlayerName));
                    selectedPanels.Remove(choosenPanel);
                }
                else
                {
                    choosenPanel.BackColor = Color.LightBlue;
                    selectedPanels.Add(choosenPanel);
                }

                choosenPanel.DoDragDrop(choosenPanel, DragDropEffects.Copy);
            }
        }

        private void clearSelection_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        private void clearSelectionFavorites_Click(object sender, EventArgs e)
        {
            favoritePlayers?.Clear();
            ShowFavoritePlayersData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(resources.GetString("appClosing"), "Close Application", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                if (favoritePlayers.Count == 3)
                {
                    SaveUserData();
                }
            }
        }

        private async void SortLists()
        {
            List<Match> matches = await footballRepository.GetMatchesAsync(choosenTeam);

            allPlayers.ToList().ForEach(p
                => matches.ForEach(m
                =>
                {
                    p.Goals += m.HomeTeamEvents.Where(e
                    => e.TypeOfEvent == TypeOfEvent.Goal && e.Player == p.Name).Count() +
                    m.AwayTeamEvents.Where(e
                    => e.TypeOfEvent == TypeOfEvent.Goal && e.Player == p.Name).Count();

                    p.YellowCards += m.HomeTeamEvents.Where(e
                    => (e.TypeOfEvent == TypeOfEvent.YellowCard || e.TypeOfEvent == TypeOfEvent.YellowCardSecond) && e.Player == p.Name).Count() +
                    m.AwayTeamEvents.Where(e
                    => (e.TypeOfEvent == TypeOfEvent.YellowCard || e.TypeOfEvent == TypeOfEvent.YellowCardSecond) && e.Player == p.Name).Count();
                }));


            List<Player> sortedPlayers = new List<Player>(allPlayers);

            sortedPlayers.Sort((x, y) => -x.Goals.CompareTo(y.Goals));
            sortedByGoals = new List<Player>(sortedPlayers);

            sortedPlayers.Sort((x, y) => -x.YellowCards.CompareTo(y.YellowCards));
            sortedByYellowCards = new List<Player>(sortedPlayers);

            matches.Sort((x, y) => -x.Attendance.CompareTo(y.Attendance));
            sortedByVisitors = new List<Match>(matches);
        }

        private void btnRankGoals_Click(object sender, EventArgs e)
        {
            new RangForm(sortedByGoals, RankingType.Goals).ShowDialog();
        }

        private void btnRankYellowCards_Click(object sender, EventArgs e)
        {
            new RangForm(sortedByYellowCards, RankingType.YellowCards).ShowDialog();
        }

        private void btnRankVisitors_Click(object sender, EventArgs e)
        {
            new RangForm(sortedByVisitors).ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ChampionshipType oldType = settingsRepository.GetSettings().Type;
            if(new SettingsForm().ShowDialog() == DialogResult.OK)
            {
                if (oldType != settingsRepository.GetSettings().Type)
                {
                    settingsRepository.DeleteUserData();
                }
            }
        }

        private void setPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    PlayerUserControl pnl = owner.SourceControl as PlayerUserControl;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Player player = allPlayers.ToList().Find(p => p.Name == pnl.PlayerName);
                        player.PicturePath = ofd.FileName;
                        pnl.pictureBox1.Image = Image.FromFile(ofd.FileName);
                        if (favoritePlayers.Contains(player))
                        {
                            favoritePlayers.ToList().Find(p => p.Name == pnl.PlayerName).PicturePath = ofd.FileName;
                            ShowFavoritePlayersData();
                        }
                    }
                }
            }
        }
    }
}
