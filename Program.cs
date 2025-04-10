using System.Net.NetworkInformation;
using Microsoft.Data.SqlClient;

namespace Hand_in_W15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        
            connection.Open();

            List<int> actorIds = GetAndPrintActorIds(connection, GetStringInput("förnamn"), GetStringInput("efternamn"));
            if (actorIds.Count == 1)
            {
                GetAndPrintFilms(connection, actorIds[0]);
            }

            connection.Close();
        }

        private static void GetAndPrintFilms(SqlConnection connection, int actorId)
        {
            string filmQuery = "select title " +
                                "from film " +
                                "inner join film_actor on film.film_id = film_actor.film_id " +
                                $"where film_actor.actor_id = {actorId}";
        }

        private static List<int> GetAndPrintActorIds(SqlConnection connection, string firstName, string lastName)
        {
            string actorQuery = "select actor_id, first_name,last_name " +
                                "from actor " +
                                "where first_name like 'susan' and last_name like 'davis'";
            List<int> actorIds = new();
            var actorcommand = new SqlCommand(actorQuery, connection);
            var actorReader = actorcommand.ExecuteReader();
            
            if (actorReader.HasRows)
            {
                while (actorReader.Read())
                {
                    actorIds.Add((int)actorReader[0]);
                    Console.WriteLine($"Id: {actorReader[0]} Namn: {actorReader[1]} {actorReader[2]}");
                }
            }
            else
            {
                Console.WriteLine("Det finns ingen skådespelare med det namnet.");
            }
            return actorIds;
        }

        private static string GetStringInput(string name)
        {
            Console.Write($"Skriv {name} på skådespelaren du söker:");
            return Console.ReadLine() ?? "";
        }
    }
}
