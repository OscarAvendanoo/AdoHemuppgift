using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdoHemuppgift
{
    internal class DatabaseHandler
    {
        public string Connection { get; set; }
        public DatabaseHandler(string connection)
        {
            this.Connection = connection;
        }
        public List<string> GetAllFilmsFromActorByName(string firstname, string lastname)
        {

            List<string> films = new List<string>();

            // Använder using här istället för "Close()" då det verkar vara best practise när man jobbar med Idispoable objekt

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string quary = $"select title from film " +
                    $"inner join film_actor as fa on fa.film_id = film.film_id " +
                    $"inner join actor as a on a.actor_id = fa.actor_id " +
                    $"where a.first_name = '{firstname}' and a.last_name = '{lastname}'";

                using (SqlCommand getFilmFromActorCommand = new SqlCommand(quary, connection))
                {
                    connection.Open();
                    var actorFilms = getFilmFromActorCommand.ExecuteReader();
                    if (actorFilms.HasRows)
                    {
                        while (actorFilms.Read())
                        {
                            films.Add($"{actorFilms.GetString(0)}");
                        }
                    }

                    return films;
                }
            }
        }

        public List<string> GetAllFilmsFromActorByID(int id)
        {

            List<string> films = new List<string>();

            // Använder using här istället för "Close()" då det verkar vara best practise när man jobbar med Idispoable objekt

            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string quary = $"select title from film " +
                               $"inner join film_actor as fa on fa.film_id = film.film_id " +
                               $"inner join actor as a on a.actor_id = fa.actor_id where a.actor_id = {id}";

                using (SqlCommand getFilmFromActorCommand = new SqlCommand(quary, connection))
                {
                    connection.Open();
                    var actorFilms = getFilmFromActorCommand.ExecuteReader();
                    if (actorFilms.HasRows)
                    {
                        while (actorFilms.Read())
                        {
                            films.Add($"{actorFilms.GetString(0)}");
                        }
                    }

                    return films;
                }
            }
        }
        public List<string> GetActorsID(string firstname, string lastname)
        {
            List<string> Actors = new List<string>();
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string quary = $"select actor_id, first_name, last_name from actor " +
                               $"where first_name = '{firstname}' and last_name = '{lastname}'";

                using (SqlCommand getActorAndIdCommand = new SqlCommand(quary, connection))
                {
                    connection.Open();
                    var ActorsAndID = getActorAndIdCommand.ExecuteReader();
                    if (ActorsAndID.HasRows)
                    {
                        while (ActorsAndID.Read())
                        {
                            Actors.Add($"{ActorsAndID.GetInt32(0)}. {ActorsAndID.GetString(1)} {ActorsAndID.GetString(2)}");

                        }
                    }
                }
            }
            return Actors;
        }
        public int CheckIfActorExist(string firstname, string lastname)
        {
            int count = 0;
            string quary = $"select count (*) from actor " +
                           $"where first_name = '{firstname}' and last_name = '{lastname}'";

            // Använder using här istället för "Close()" då det verkar vara best practise när man jobbar med Idispoable objekt
            using (SqlConnection connection = new SqlConnection(Connection))
            {

                using (SqlCommand getActorCommand = new SqlCommand(quary, connection))
                {
                    connection.Open();
                    count = (int)getActorCommand.ExecuteScalar();

                    return count;

                }
            }
        }

    }
}