    namespace webMyStoreApp.Components.Services
{
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using webMyStoreApp.Authentication;

    public class UserAccountService
    {
        private readonly string _connString;

        public UserAccountService(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<User> GetUsers()
        {
            var users = new List<User>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT UserId, Username, Password FROM Users", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                Password = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return users;
        }
    }
}
