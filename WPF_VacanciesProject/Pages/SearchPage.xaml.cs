using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WPF_VacanciesProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        ModelEntity db = new ModelEntity();
        string conncectionString = "";

        public SearchPage()
        {
            InitializeComponent();

            conncectionString = @"data source=LAPTOP-NSMJD0QF\SQLEXPRESS;initial catalog=VacanciesDB;integrated security=True";

            cbCategory.ItemsSource = db.CategoryVacanciesList.ToList();
        }

        private CategoryVacanciesList cbCategorySelectionChanged()
        {
            CategoryVacanciesList cvl = (CategoryVacanciesList)cbCategory.SelectedItem;

            return cvl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TempClass> list = new List<TempClass>();
            var tableName = cbCategorySelectionChanged().CategoryName;
            var pubDate = tbxDate.Text;
            var email = tbxEmail.Text;
            var query = "SELECT * FROM " + tableName + " WHERE  author = '" + email + "' AND pubDate = '"+pubDate+"' ";


            SqlConnection con = new SqlConnection(conncectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;
            try
            {
                con.Open();

                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable.DataContext = ds.Tables[0].DefaultView;
                tbxError.Text = "Результаты поиска успешно сформировались";
                

                con.Close();
            }
            catch(Exception ex)
            {
                tbxError.Text = "Нет данных, подходящих под критерии поиска";
                tbxError.Text = ex.Message;
            }

            
        }
    }
}
