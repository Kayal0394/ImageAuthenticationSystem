using System;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace ImageAuthenticationSystem.DAL
{
    public class DBConnection
    {
        string connectionString;

        public DBConnection()
        {
            try
            {
                connectionString = "Server=localhost;Port=3306;Database=IAS;User Id=root;Password=root1234;";
            }
            catch (Exception ex) { }
        }

        public MySqlConnection StartConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return connection;
        }

        public void EndConnection(MySqlConnection connection)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex) { }
        }
    }
}

