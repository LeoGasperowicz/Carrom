using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Carrom
{
    public class MariaDB : Connection
    {
        private MySqlConnection connection;

        public MariaDB()
        {
            string connectionString = "server=localhost;port=3306;database=nom_de_votre_base;user=votre_utilisateur;password=votre_mot_de_passe";
            connection = new MySqlConnection(connectionString);
        }

        public override void openConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Successful connection to MariaDB");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error opening connection to MariaDB: {ex.Message}");
            }
        }

        public override void closeConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Connection to MariaDB closed successfully");
            }
            catch (MySqlException ex)
            {
                // Gérer l'exception
                Console.WriteLine($"Error closing connection to MariaDB: {ex.Message}");
            }
        }

        public override List<Player> queryUsers()
        {
            
            // return a list of players
            return new List<Player>();
        }
    }
}
