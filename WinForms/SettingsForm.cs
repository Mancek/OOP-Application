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

namespace WinForms
{
    public partial class SettingsForm : Form
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsForm));
        private const string HR = "hr", EN = "en";
        public SettingsForm()
        {
            InitializeComponent();
            PrepareComboBox();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(resources.GetString("msgBoxSave"), "Application Settings", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                RepositoryFactory.GetSettingsRepository().SaveSettings(new Data.Models.Settings
                {
                    Language = (string)cbLanguage.SelectedItem,
                    Type = (ChampionshipType)cbChampionshipType.SelectedItem
                });
            }
            Dispose();

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
