using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Carrom
{
    public class PostgreSQL : Connection
    {
        private NpgsqlConnection connection;

        public PostgreSQL()
        {
            string connectionString = "Host=localhost;Port=5432;Username=votre_utilisateur;Password=votre_mot_de_passe;Database=nom_de_votre_base";
            connection = new NpgsqlConnection(connectionString);
        }

        public override void openConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection to PostgreSQL opened successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening connection to PostgreSQL: {ex.Message}");
            }
        }

        public override void closeConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Connection to PostgreSQL closed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection to PostgreSQL: {ex.Message}");
            }
        }

        public override List<Player> queryUsers()
        {

            // return a list of players
            return new List<Player>();
        }
    }
}
