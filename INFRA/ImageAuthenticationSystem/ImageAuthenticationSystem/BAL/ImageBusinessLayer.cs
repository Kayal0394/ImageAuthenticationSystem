using System;
using System.Data;
using ImageAuthenticationSystem.DAL;
using ImageAuthenticationSystem.Model;
using MySql.Data.MySqlClient;

namespace ImageAuthenticationSystem.BAL
{
	public class ImageBusinessLayer
	{
		public ImageBusinessLayer()
		{
		}

		public string CreateUser(ImageAuthenticationModel model)
		{
            string sStatus = "";
            DBConnection dBConnection = new DBConnection();
            try
            {
                string firstName = model.FirstName;
                string lastName = model.LastName;
                string email = model.Email;
                string password = string.Join(",", model.SelectedImageNames);

                // Call the stored procedure to insert the user
                using (MySqlConnection con = dBConnection.StartConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("InsertUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_FirstName", firstName);
                        cmd.Parameters.AddWithValue("@p_LastName", lastName);
                        cmd.Parameters.AddWithValue("@p_EmailId", email);
                        cmd.Parameters.AddWithValue("@p_PasswordId", password);

                        // Execute the command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Read the status message returned by the stored procedure
                                sStatus = reader["StatusMessage"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sStatus = "Error: " + ex.Message;
            }

            return sStatus;
        }

        public List<string> GetUserPassword(ImageAuthenticationModel model)
        {
            DBConnection dBConnection = new DBConnection();
            List<string> lstPassword = null;

            try
            {
                string email = model.Email;

                // Call the stored procedure to insert the user
                using (MySqlConnection con = dBConnection.StartConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("InsertUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_EmailId", email);

                        // Execute the command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Read the status message returned by the stored procedure
                                string sPassword = reader["StatusMessage"].ToString();
                                lstPassword = new List<string>(sPassword?.Split(','));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return lstPassword;
        }

        public ImageAuthenticationModel ValidateUser(ImageAuthenticationModel model)
        {
            DBConnection dBConnection = new DBConnection();
            try
            {
                string email = model.Email;
                string password = string.Join(",", model.SelectedImageNames);

                // Call the stored procedure to insert the user
                using (MySqlConnection con = dBConnection.StartConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("InsertUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_EmailId", email);
                        cmd.Parameters.AddWithValue("@p_PasswordId", password);

                        // Execute the command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Read the status message returned by the stored procedure
                                model.Status = reader["StatusMessage"].ToString();
                                model.Status = reader["FirstName"].ToString();
                                model.Status = reader["LastName"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.Status = "Error: " + ex.Message;
            }

            return model;
        }

    }
}

