using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdoHemuppgift
{
    internal class DatabaseHandler
    {
        public SqlConnection DbConnection { get; set; }
        public DatabaseHandler(SqlConnection dbConnecttion)
        {
            this.DbConnection = dbConnecttion;  
        }
        public List<string> GetAllActors()
        {
            List<string> allActorsList = new List<string>();

            // valde att hämta allt i actortabellen så att metoden kan användas i flera syften 

            var getActorsCommand = new SqlCommand("select * from actor", DbConnection);
            DbConnection.Open();
            var allActors = getActorsCommand.ExecuteReader();
            if (allActors.HasRows)
            {
                while (allActors.Read())
                {
                    allActorsList.Add($"{allActors.GetInt32(0)}. {allActors.GetString(1)} {allActors.GetString(2)}");
                }
            }

            DbConnection.Close();

            return allActorsList;

        }
       
    }
}
