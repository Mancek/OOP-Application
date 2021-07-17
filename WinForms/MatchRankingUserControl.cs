using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class MatchRankingUserControl : UserControl
    {
        public string MatchLocation
        {
            get => lbLocation.Text;
            set => lbLocation.Text = value;
        }
        public string MatchAttendance
        {
            get => lbAttendance.Text;
            set => lbAttendance.Text = value;
        }
        public string MatchHomeTeam
        {
            get => lbHomeTeam.Text;
            set => lbHomeTeam.Text = value;
        }
        public string MatchAwayTeam
        {
            get => lbAwayTeam.Text;
            set => lbAwayTeam.Text = value;
        }
        public MatchRankingUserControl()
        {
            InitializeComponent();
        }
    }
}
