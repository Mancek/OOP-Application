using Data.DAL;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly ISettingsRepository settingsRepository;
        private const string HR = "hr", EN = "en";
        public SettingsWindow()
        {
            settingsRepository = RepositoryFactory.GetSettingsRepository();
            InitializeComponent();
            PrepareComboBox();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure", "Application Settings", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                settingsRepository.SaveScreenSize(new Screen
                {
                    Type = (ScreenSizeType)cbScreenSize.SelectedItem
                });
            }
            Close();
        }

        private void PrepareComboBox()
        {
            cbChampionshipType.Items.Add(ChampionshipType.Men);
            cbChampionshipType.Items.Add(ChampionshipType.Women);
            cbChampionshipType.SelectedIndex = 0;

            cbLanguage.Items.Add(HR);
            cbLanguage.Items.Add(EN);
            cbLanguage.SelectedIndex = 0;
            if (settingsRepository.DoSettingsExist())
            {
                Settings settings = settingsRepository.GetSettings();
                cbLanguage.SelectedItem = settings.Language;
                cbChampionshipType.SelectedItem = settings.Type;
                cbLanguage.IsEnabled = false;
                cbChampionshipType.IsEnabled = false;
            }
            ScreenSizeType[] screenSizes = (ScreenSizeType[])Enum.GetValues(typeof(ScreenSizeType));
            screenSizes.ToList().ForEach(s => cbScreenSize.Items.Add(s));
        }
    }
}
