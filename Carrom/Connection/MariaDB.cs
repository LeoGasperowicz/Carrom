﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using MySqlConnector;
using MySqlX.XDevAPI.Common;
using Npgsql.Replication;

namespace Carrom
{
    public class MariaDB
    {
        //private MySqlConnection connection;
        string MyConnectionString = "server=127.0.0.1;user=root;password=1234;port=3307";


        public MariaDB()
        {
            MySqlConnection connection = new MySqlConnection(MyConnectionString);
        }


        public void testConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(MyConnectionString);
                connection.Open();
                var stm = "SELECT VERSION()";
                var cmd = new MySqlCommand(stm, connection);

                var version = cmd.ExecuteScalar().ToString();
                Console.WriteLine($"MariaDB version: {version}");
            }
            catch (Exception ex) { }

        }
        public void createDB()
        {
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `Carrom`;";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");
            }
        }
        public void tryCreateAlterTable()
        {
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "USE Carrom";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DROP TABLE IF EXISTS player";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS player (id INT AUTO_INCREMENT    PRIMARY KEY, name VARCHAR(255), password VARCHAR(100))";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");

                //cmd.CommandText = "ALTER TABLE player ADD COLUMN NumVictory VARCHAR(255)";
                //response = cmd.ExecuteNonQuery();
                //Console.WriteLine($"Response: {response}");
            }
        }
        public void createScoreTable()
        {
            string retour = "";
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "USE Carrom;";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DROP TABLE IF EXISTS score;";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS score (
                id INT AUTO_INCREMENT PRIMARY KEY,
                idP int,
                date DATETIME,
                score INT,
                CONSTRAINT fk_username FOREIGN KEY (idP) REFERENCES player(id)
            );";
                cmd.ExecuteNonQuery();
                retour = "Score table created successfully.";
            }
        }
        public void createUser(string name, string password)
        {
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "USE Carrom";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO player(name,password) VALUES(@person, @mdp)";
                cmd.Parameters.AddWithValue("@person", name);
                cmd.Parameters.AddWithValue("@mdp", password);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public bool isExist(string name)
        {
            bool exist = false;
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "USE Carrom";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT name FROM player WHERE name = @person";
                    cmd.Parameters.AddWithValue("@person", name);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        exist = true;
                    }
                }
                catch (Exception ex)
                {

                    // handle exception here
                }
                finally
                {

                    conn.Close();
                }
            }
            return exist;
        }
        public List<string> listTable() 
        {
            List<string> allPlayers = new List<string>();
            var connection = new MySqlConnection(MyConnectionString);
            using (var cmd = connection.CreateCommand())
            try
            {
                connection.Open();
                cmd.CommandText = "USE Carrom";
                cmd.ExecuteNonQuery();
                MySqlCommand command = new MySqlCommand("SELECT name FROM player", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            // access your record colums by using reader
                            allPlayers.Add(reader["name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // handle exception here
            }
            finally
            {
                connection.Close();
            }
            return allPlayers;  
        }
        public void deletePlayer(string name)
        {
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "USE Carrom";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete from player where name = @name";
                cmd.Parameters.AddWithValue("@name", name);
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");
           }
        }
        public bool checkUser(string name, string password)
        {
            bool result = false;
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
                try
                {
                    conn.Open();
                    cmd.CommandText = "USE Carrom";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT name FROM player where name = @name AND password = @password";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reader.Close();
                            result = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    
                    // handle exception here
                }
                finally
                {
                    
                    conn.Close();
                }
            return result;
        }
        public void saveGame(int idp, DateTime time, int score)
        {
            using (MySqlConnection conn = new MySqlConnection(MyConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.CommandText = "USE Carrom";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "INSERT INTO score(idP, date, score) VALUES(@idP, @date, @score)";
                        cmd.Parameters.AddWithValue("@idP", idp);
                        cmd.Parameters.AddWithValue("@date", time);
                        cmd.Parameters.AddWithValue("@score", score);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle exceptions as needed
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
            }
        }
        public int getIdPlayer(string name)
        {
            int idP = 0;
            using (var conn = new MySqlConnection(MyConnectionString))
            using (var cmd = conn.CreateCommand())
                try
                {
                    conn.Open();
                    cmd.CommandText = "USE Carrom";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT id FROM player where name = @name";
                    cmd.Parameters.AddWithValue("@name", name);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idP = (int)reader["id"];
                            reader.Close();                        
                        }
                    }
                }
                catch (Exception ex)
                {
                    // handle exception here
                }
                finally
                {
                    conn.Close();
                }
            return idP;
        }
        public (bool success, string name, int bestScore, DateTime date) searchBestScore(string playerName)
        {
            string query = "SELECT name, Max(Score) AS BestScore, date FROM score LEFT JOIN player ON score.idP = player.id WHERE NAME = @PlayerName GROUP BY name, date ORDER BY BestScore DESC LIMIT 1";

            using (MySqlConnection conn = new MySqlConnection(MyConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    
                    try
                    {
                        conn.Open();
                        cmd.CommandText = "USE Carrom";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@PlayerName", playerName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                int bestScore = Convert.ToInt32(reader["BestScore"]);
                                DateTime scoreDate = Convert.ToDateTime(reader["date"]);
                                return (true, name, bestScore, scoreDate);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or handle exceptions as needed
                        Console.WriteLine($"Database error: {ex.Message}");
                    }
                }
            }
            return (false, null, 0, DateTime.MinValue); ; 
        }
    }
}
