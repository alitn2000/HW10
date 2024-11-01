
using HW10.ConnectionString;
using HW10.ENTITIES;
using HW10.Enums;
using HW10.Querys;
using HW10.Repository;
using System.Data;
using System.Data.SqlClient;

public class SqlRepository_ADO : IRepository
{
    public void AddUser(User user)
    {
        using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Query.Create, connect))
            {
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Status", (int)user.Status);

                connect.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<User> GetAll()
    {
        var users = new List<User>();

        using (var connection = new SqlConnection(Connection.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(Query.GetAll, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["Id"];
                        user.UserName = (string)reader["UserName"];
                        user.Password = (string)reader["Password"];
                        user.Status = (StatusEnum)reader["Status"];
                        users.Add(user);
                    }
                }
            }
            connection.Close();
            return users;
        }


    }

    public User GetByName(string username)
    {
        using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
        {
            SqlCommand cmd = new SqlCommand(Query.GetByName, connect);
            cmd.Parameters.AddWithValue("@UserName", username);
            connect.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            if (reader.Read())
            {
                return new User()
                {
                    Id = (int)reader["Id"],
                    UserName = (string)reader["Username"],
                    Password = (string)reader["Password"],
                    Status = (StatusEnum)reader["Status"]

                };
            }

            return null;
        }
    }


    public void UpdateUser(User user)
    {
        using (var connection = new SqlConnection(Connection.ConnectionString))
        {
            connection.Open();

            using (var command = new SqlCommand(Query.UpdateUser, connection))
            {
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Status", user.Status);
                command.ExecuteNonQuery();
            }
        }
    }
}
