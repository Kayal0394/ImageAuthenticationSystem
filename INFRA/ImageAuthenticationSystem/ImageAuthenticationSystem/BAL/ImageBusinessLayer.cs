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
                string firstName = model?.FirstName;
                string lastName = model?.LastName;
                string email = model.EmailId;
                string password = string.Join(",", model.SelectedImageNames);
                string passwordList = string.Join(",", model.PasswordList);
                string encryptedPassword = AESEncryption.EncryptStringToBytes(password, email);

                // Call the stored procedure to insert the user
                using (MySqlConnection con = dBConnection.StartConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_CreateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_FirstName", firstName);
                        cmd.Parameters.AddWithValue("@p_LastName", lastName);
                        cmd.Parameters.AddWithValue("@p_EmailId", email);
                        cmd.Parameters.AddWithValue("@p_Password", encryptedPassword);
                        cmd.Parameters.AddWithValue("@p_PasswordList", passwordList);

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
            List<string> lstPassword = new List<string>();

            try
            {
                string email = model.EmailId;

                // Call the stored procedure to insert the user
                using (MySqlConnection con = dBConnection.StartConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_GetUserPassword", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_EmailId", email);

                        // Execute the command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Read the status message returned by the stored procedure
                                if (Convert.ToInt32(reader["StatusMessage"]) > 0)
                                {
                                    string password = reader["PasswordList"].ToString();
                                    lstPassword = new List<string>(password?.Split(','));
                                }
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
                string email = model.EmailId;
                string password = string.Join(",", model.SelectedImageNames);
                string encryptedPassword = AESEncryption.EncryptStringToBytes(password, email);

                // Call the stored procedure to insert the user
                using (MySqlConnection con = dBConnection.StartConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand("SP_ValidatePassword", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_EmailId", email);
                        cmd.Parameters.AddWithValue("@p_Password", encryptedPassword);

                        // Execute the command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Read the status message returned by the stored procedure
                                model.Status = reader["status"].ToString();
                                model.Message = reader["StatusMessage"].ToString();
                                if (model.Status.Equals("1")) {
                                    model.FirstName = reader["FirstName"].ToString();
                                    model.LastName = reader["LastName"].ToString();
                                }
                               
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

