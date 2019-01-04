using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_VacanciesProject
{
    class TempClass
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public DateTime PubDate { get; set; }

        public string Email { get; set; }

        public TempClass()
        {

        }

        //public TempClass(IDataRecord record)
        //{
        //    this.Init(record);
        //}
        //public void Init(IDataRecord record)
        //{
        //    Title = (string)record["title"];
        //    Link = (string)record["link"];
        //    Description = (string)record["description"];
        //    PubDate = (DateTime)record["pubDate"];
        //    Email = (string)record["author"];
        //}
    }
}
