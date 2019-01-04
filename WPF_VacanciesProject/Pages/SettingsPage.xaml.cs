using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Xml.Serialization;

namespace WPF_VacanciesProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        ModelEntity db = new ModelEntity();
        string conncectionString = "";

        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppSettings));

        private string pathToSettings = "appSetting.xml";

        public SettingsPage()
        {
            InitializeComponent();

            conncectionString = @"data source=LAPTOP-NSMJD0QF\SQLEXPRESS;initial catalog=VacanciesDB;integrated security=True";

            cbCategory.ItemsSource = db.CategoryVacanciesList.ToList();     
            lblInfo.Content = "Выберите таблицу \nдля отображения информации";



            AppSettings appSet = new AppSettings();

            FileInfo fi = new FileInfo(pathToSettings);
            if (fi.Exists)
            {
                using (FileStream stream = new FileStream(pathToSettings, FileMode.Open))
                {
                    appSet = (AppSettings)xmlSerializer.Deserialize(stream);
                }
            }

            spAppSet.DataContext = appSet;


        }

        private CategoryVacanciesList cbCategorySelectionChanged()
        {
            CategoryVacanciesList cvl = (CategoryVacanciesList)cbCategory.SelectedItem;

            return cvl;
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbxSecond.Text = cbCategorySelectionChanged().CategoryName;
            tbxThird.Text = GetCountRecors(tbxSecond.Text).ToString();
        }

        private int GetCountRecors(string tableName)
        {
            int cnt = 0;

            tableName = cbCategorySelectionChanged().CategoryName;
            var query = "SELECT COUNT(*) FROM " + tableName;

            List<TempClass> list = new List<TempClass>();

            SqlConnection con = new SqlConnection(conncectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                cnt = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                tbxError.Text = ex.Message;
            }

            return cnt;
        }

          
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            AppSettings appSet = (AppSettings)spAppSet.DataContext;

            using (FileStream stream = new FileStream(pathToSettings, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(stream, appSet);
            }
        }


    }
}
