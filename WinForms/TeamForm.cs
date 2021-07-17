using Data.DAL;
using Data.Models;
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
    public partial class TeamForm : Form
    {
        private readonly IFootballRepository footballRepository;
        public TeamForm()
        {
            InitializeComponent();
            footballRepository = RepositoryFactory.GetFootballRepository();
            PrepareComboBox();
        }

        private async void PrepareComboBox()
        {
            List<Team> teams = await footballRepository.GetTeamsAsync();
            teams.ForEach(t => cbTeams.Items.Add(t));
            cbTeams.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ((MainForm)Owner).SetTeam((Team)cbTeams.SelectedItem);
            Dispose();
        }

    }
}
