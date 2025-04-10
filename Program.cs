using System.Net.NetworkInformation;
using Microsoft.Data.SqlClient;

namespace Hand_in_W15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            string firstName = GetStringInput("förnamn");
            string lastName = GetStringInput("efternamn");
            string actorQuery = "select actor_id, first_name,last_name " +
                                "from actor " +
                                "where first_name like 'susan' and last_name like 'davis'";
            var Actorcommand = new SqlCommand(actorQuery, connection);

            connection.Open();
            
            var Actorreader = Actorcommand.ExecuteReader();

            if (Actorreader.HasRows)
            {
                while (Actorreader.Read())
                {
                    Console.WriteLine($"Id: {Actorreader[0]} Namn: {Actorreader[1]} {Actorreader[2]}");
                }
            }
            else
            {
                Console.WriteLine("Det finns ingen skådespelare med det namnet.");
            }

            connection.Close();
        }

        private static string GetStringInput(string name)
        {
            Console.Write($"Skriv {name} på skådespelaren du söker:");
            return Console.ReadLine() ?? "";
        }
    }
}
