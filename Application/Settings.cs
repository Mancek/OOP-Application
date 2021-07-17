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

namespace Application
{
    public partial class Settings : Form
    {
        private const string HR = "hr", EN = "en";
        public Settings()
        {
            InitializeComponent();
            PrepareComboBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RepositoryFactory.GetSettingsRepository().SaveSettings(new Data.Models.Settings
            {
                Language = (string)cbLanguage.SelectedItem,
                Type = (ChampionshipType)cbChampionshipType.SelectedItem
            });
        }

        private void PrepareComboBox()
        {
            cbChampionshipType.Items.Add(ChampionshipType.Men);
            cbChampionshipType.Items.Add(ChampionshipType.Women);
            cbChampionshipType.SelectedIndex = 0;

            cbLanguage.Items.Add(HR);
            cbLanguage.Items.Add(EN);
            cbLanguage.SelectedIndex = 0;
        }
    }
}
