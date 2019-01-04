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
using WPF_VacanciesProject.Pages;

namespace WPF_VacanciesProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Frame mf { get; set; }

        ModelEntity db = new ModelEntity();
        public MainWindow()
        {
            InitializeComponent();

            mf = MainFrame;

            MainWindow.mf.Source = new Uri("Pages/VacanciesCategoryList.xaml", UriKind.RelativeOrAbsolute);
        }

        private void SearchMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SearchPage sp = new SearchPage();
            MainWindow.mf.Source = new Uri("SearchPage.xaml", UriKind.RelativeOrAbsolute);
            MainWindow.mf.NavigationService.Navigate(sp);
        }

        private void CategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            VacanciesCategoryList vp = new VacanciesCategoryList();
            MainWindow.mf.Source = new Uri("VacanciesCategoryList.xaml", UriKind.RelativeOrAbsolute);
            MainWindow.mf.NavigationService.Navigate(vp);
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage stp = new SettingsPage();
            MainWindow.mf.Source = new Uri("SettingsPage.xaml", UriKind.RelativeOrAbsolute);
            MainWindow.mf.NavigationService.Navigate(stp);
        }
    }
}
