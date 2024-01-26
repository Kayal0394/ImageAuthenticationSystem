using System;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace ImageAuthenticationSystem.DAL
{
    public class DBConnection
    {
        string connectionString = "Server=localhost;Port=3306;Database=IAS;User Id=root;Password=root1234;";

        public MySqlConnection StartConnection()
        {
            MySqlConnection connection;
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return connection;

        }

        public void EndConnection(MySqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}

