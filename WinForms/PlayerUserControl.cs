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
    public partial class PlayerUserControl : UserControl
    {
        public string PlayerName 
        { 
            get => lbName.Text; 
            set => lbName.Text = value; 
        }
        public string PlayerNumber
        {
            get => lbNumber.Text;
            set => lbNumber.Text = value;
        }
        public string PlayerPosition
        {
            get => lbPosition.Text;
            set => lbPosition.Text = value;
        }
        public string PlayerCaptain
        {
            get => lbCaptain.Text;
            set => lbCaptain.Text = value;
        }
        public PlayerUserControl()
        {
            InitializeComponent();
        }
    }
}
