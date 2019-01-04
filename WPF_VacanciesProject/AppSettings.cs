using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VacanciesProject
{
    [Serializable]
    public class AppSettings
    {
        public string DataSource { get; set; }
        public string DatabaseName { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string ConnectionString { get; set; }
        public string PathToFile { get; set; }
    }
}
