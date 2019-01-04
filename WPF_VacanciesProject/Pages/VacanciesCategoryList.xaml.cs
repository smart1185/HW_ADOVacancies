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
using System.Xml.Linq;

namespace WPF_VacanciesProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для VacanciesCategoryList.xaml
    /// </summary>
    public partial class VacanciesCategoryList : Page
    {
        private static ModelEntity db;
        private static string connectionString;
        public VacanciesCategoryList()
        {
            InitializeComponent();
            connectionString = @"data source=LAPTOP-NSMJD0QF\SQLEXPRESS;initial catalog=VacanciesDB;integrated security=True";

            db = new ModelEntity();
            lbCategoryList.ItemsSource = db.CategoryVacanciesList.ToList();

        }

        private void lbCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryVacanciesList cvl = (CategoryVacanciesList)((ListBox)sender).SelectedItem;
            cvl.CategoryName = tbxName.Text;
            cvl.CategoryLink = tbxLink.Text;
        }

        private static List<TempClass> GetFromXML(string pathToXML)
        {            
            List<TempClass> items = new List<TempClass>();
            try
            {
                XDocument xdoc = XDocument.Load(pathToXML);

                foreach (XElement i in xdoc.Element("rss").Element("channel").Elements("item"))
                {
                    TempClass data = new TempClass();
                    data.Title = i.Element("title").Value;
                    data.Link = i.Element("link").Value;
                    data.Description = i.Element("description").Value;
                    data.PubDate= Convert.ToDateTime(i.Element("pubDate").Value);
                    data.Email = i.Element("author").Value;

                    items.Add(data);
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);                
            }

            return items;
        }

        private void bntSave_Click(object sender, RoutedEventArgs e)
        {
           
            string tableName = tbxName.Text;
            string pathToXML = tbxLink.Text;
            tbxResult.Text = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "CREATE TABLE " + tableName +
                "(id int PRIMARY KEY IDENTITY(1,1), title VARCHAR(200), link VARCHAR(max), description VARCHAR(max), pubDate DATETIME, author VARCHAR(200))";                  
                SqlCommand sqlCom = new SqlCommand(query, con);



                string query1 = "sp_InsertIntoCategoriesList";
                SqlCommand cmd = new SqlCommand(query1, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter par = new SqlParameter
                {
                    ParameterName = "@CategoryName",
                    Value = tableName
                };

                cmd.Parameters.Add(par);

                SqlParameter par1 = new SqlParameter
                {
                    ParameterName = "@CategoryLink",
                    Value = pathToXML
                };
                cmd.Parameters.Add(par1);



                List<TempClass> list = new List<TempClass>();
                list = GetFromXML(pathToXML);
                SqlCommand insertCmd=null;
                string query2 = "";
                foreach (var item in list)
                {                    
                    var title = item.Title;
                    var link = item.Link;
                    var description = item.Description;
                    var pubDate = item.PubDate;
                    var email = item.Email;

                    query2 += "INSERT INTO " + tableName + " VALUES('"+title+"', '"+link+"', '"+description+"', '"+pubDate+"', '"+email+"')";
                 
                }
                insertCmd = new SqlCommand(query2, con);

                try
                {
                    con.Open();

                    sqlCom.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    insertCmd.ExecuteNonQuery();


                    con.Close();

                    tbxResult.Text = "Таблица создана, запись успешно добавлена, данные в таблицу занесены";
                }
                catch (Exception ex)
                {
                    tbxResult.Text = "Таблица не создана";
                    tbxResult.Text = ex.Message;
                }
            } 
              
            lbCategoryList.ItemsSource = db.CategoryVacanciesList.ToList();

            tbxName.Text = "";
            tbxLink.Text = "";
            tbxResult.Text = "";
        }
    }
}
