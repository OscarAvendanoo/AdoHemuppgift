using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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
        public List<string> GetAllFilmsFromActor(string actorId)
        {
            List<string> films = new List<string>();

            var getFilmFromActorCommand = new SqlCommand($"select title from film inner join film_actor as fa on fa.film_id = film.film_id inner join actor as a on a.actor_id = fa.actor_id where a.actor_id = {actorId}", DbConnection);
            DbConnection.Open();
            var actorFilms = getFilmFromActorCommand.ExecuteReader();
            if (actorFilms.HasRows)
            {
                while (actorFilms.Read())
                {
                    films.Add($"{actorFilms.GetString(0)}");
                }   
            }
            DbConnection.Close();
            return films;
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

