using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdoHemuppgift
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Set the base path
                .AddJsonFile("appsettings.json")      // Add the JSON file
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            
            DatabaseHandler dbHandler = new DatabaseHandler(connectionString);
            ApplicationUI applicationUi = new ApplicationUI(dbHandler);
            applicationUi.RunUI();


        }
    }
}
