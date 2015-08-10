using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Investors2015.Models
{
    public class Datubaze
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        //public static async Task<List<Datubaze>> GetDatabasesFromTildeAsync()
        //{
        //    DataTable rezultati = new DataTable();
        //    List<Datubaze> datubazesList = new List<Datubaze>();

        //    #region ielasām datubāžu nosaukumus no SQL = datubazesStringList

        //    string connectionString = ConfigurationManager.ConnectionStrings["TildesJumisMaster"].ConnectionString;

        //    string asyncConnectionString =
        //        new SqlConnectionStringBuilder(connectionString) {AsynchronousProcessing = true}.ToString();


        //    using (SqlConnection konekcija = new SqlConnection(asyncConnectionString))
        //    using (SqlCommand komanda = new SqlCommand())
        //    {
        //        konekcija.Open();
        //        komanda.CommandText = "SELECT DISTINCT name FROM dbo.sysdatabases WHERE name NOT IN ('master', 'msdb','tempdb', 'model') ORDER BY name";
        //        komanda.CommandType = CommandType.Text;
        //        komanda.Connection = konekcija;
        //        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda))
        //        {   dataAdapter.Fill(rezultati); }
        //    }
        //    List<string> datubazesStringList = rezultati.AsEnumerable().Select(x => x[0].ToString()).ToList();


        //    #endregion

        //    #region veidojam MostRecent listi
        //    datubazesList.Add(new Datubaze { Id = 0, Name = "1ā", Category = "MostRecent" });
        //    datubazesList.Add(new Datubaze { Id = 1, Name = "2ā", Category = "MostRecent" });
        //    datubazesList.Add(new Datubaze { Id = 2, Name = "3ā", Category = "MostRecent" });
        //    datubazesList.Add(new Datubaze { Id = 3, Name = "4ā", Category = "MostRecent" });
        //    datubazesList.Add(new Datubaze { Id = 4, Name = "5ā", Category = "MostRecent" });
        //    #endregion

        //    #region veidojam ALL databases listi
        //    int Id = 5;
        //    foreach (string datubazeString in datubazesStringList)
        //    {
        //        datubazesList.Add(new Datubaze
        //        {
        //            Id = Id,
        //            Name = datubazeString,
        //            Category = "All"
        //        });
        //        Id++;
        //    }
        //    #endregion

        //    return datubazesList;
        //}



        public static List<Datubaze> GetDatabasesFromTilde()
        {
            DataTable rezultati = new DataTable();
            List<Datubaze> datubazesList = new List<Datubaze>();

            #region ielasām datubāžu nosaukumus no SQL = datubazesStringList
            
            
            string connectionString = ConfigurationManager.ConnectionStrings["TildesJumisMaster"].ConnectionString;
            using (SqlConnection konekcija = new SqlConnection(connectionString))
            using (SqlCommand komanda = new SqlCommand())
            {
                konekcija.Open();
                komanda.CommandText = "SELECT DISTINCT name FROM dbo.sysdatabases WHERE name NOT IN ('master', 'msdb','tempdb', 'model') ORDER BY name";
                komanda.CommandType = CommandType.Text;
                komanda.Connection = konekcija;
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda))
                { dataAdapter.Fill(rezultati); }
            }
            List<string> datubazesStringList = rezultati.AsEnumerable().Select(x => x[0].ToString()).ToList();
            #endregion

            #region veidojam MostRecent listi
            datubazesList.Add(new Datubaze { Id = 0, Name = "sol2015", Category = "MostRecent" });
            datubazesList.Add(new Datubaze { Id = 1, Name = "sol2015", Category = "MostRecent" });
            datubazesList.Add(new Datubaze { Id = 2, Name = "sol2015", Category = "MostRecent" });
            datubazesList.Add(new Datubaze { Id = 3, Name = "sol2015", Category = "MostRecent" });
            datubazesList.Add(new Datubaze { Id = 4, Name = "sol2015", Category = "MostRecent" });
            #endregion

            #region veidojam ALL databases listi
            int Id = 5;
            foreach (string datubazeString in datubazesStringList)
            {
                datubazesList.Add(new Datubaze
                {
                    Id = Id,
                    Name = datubazeString,
                    Category = "All"
                });
                Id++;
            }
            #endregion

            return datubazesList;
        }

    }
}