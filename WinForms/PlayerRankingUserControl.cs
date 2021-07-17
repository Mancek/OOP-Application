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
    public partial class PlayerRankingUserControl : UserControl
    {
        public string PlayerName
        {
            get => lbName.Text;
            set => lbName.Text = value;
        }
        public string PlayerGoals
        {
            get => lbGoals.Text;
            set => lbGoals.Text = value;
        }
        public PlayerRankingUserControl()
        {
            InitializeComponent();
        }
    }
}
