using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Models;

namespace WinForms
{
    public partial class RangForm : Form
    {
        private readonly string PICTURE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\FootballApp\\shirt.png";
        private int _printanoStranica;
        private const int pageSize = 15;

        private List<Player> players;
        public RangForm(List<Player> list, RankingType type)
        {
            InitializeComponent();
            players = list;
            CreatePlayerRankings(players, type);
            _printanoStranica = 0;
        }

        private void CreatePlayerRankings(List<Player> list, RankingType type)
        {
            flPanel.Controls.Clear();
            list.ToList().ForEach(p =>
            {
                PlayerRankingUserControl pnl = new PlayerRankingUserControl
                {
                    PlayerName = p.Name,
                    PlayerGoals = type == RankingType.Goals ? p.Goals.ToString() : p.YellowCards.ToString()
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
                flPanel.Controls.Add(pnl);
            });
        }

        public RangForm(List<Match> list)
        {
            InitializeComponent();
            CreateMatchRank(list);
            _printanoStranica = 0;
        }

        private void CreateMatchRank(List<Match> list)
        {
            flPanel.Controls.Clear();
            list.ToList().ForEach(m =>
            {
                MatchRankingUserControl pnl = new MatchRankingUserControl
                {
                    MatchLocation = m.Location,
                    MatchAttendance = m.Attendance.ToString(),
                    MatchHomeTeam = m.HomeTeam.Country,
                    MatchAwayTeam = m.AwayTeam.Country
                };
                flPanel.Controls.Add(pnl);
            });
        }

        private void miPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_printanoStranica++ == 0)
            {
                PrintControls(e);
                e.HasMorePages = true;
            }
            else
            {
                PrintControls(e);
            }
            
        }

        private void PrintControls(PrintPageEventArgs e)
        {
            var x = e.MarginBounds.Left;
            var y = e.MarginBounds.Top;
            var bmp = new Bitmap(Size.Width, Size.Height);

            List<Control> controls = new List<Control>(flPanel.Controls.Cast<Control>());
            controls.Skip((_printanoStranica - 1)* pageSize).Take(pageSize).ToList().ForEach(c =>
            {
                c.DrawToBitmap(bmp, new Rectangle
                {
                    X = 0,
                    Y = 0,
                    Width = Size.Width,
                    Height = Size.Height
                });
                e.Graphics.DrawImage(bmp, x, y);
                y += c.Height+5;
            });
        }
    }
}
