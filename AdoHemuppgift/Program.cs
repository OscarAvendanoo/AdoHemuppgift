using Microsoft.Data.SqlClient;

namespace AdoHemuppgift
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            DatabaseHandler dbHandler = new DatabaseHandler(connection);
            ApplicationUI applicationUi = new ApplicationUI(dbHandler);
            applicationUi.RunUI();


        }
    }
}
