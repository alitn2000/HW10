using Dapper;
using HW10.ConnectionString;
using HW10.ENTITIES;
using HW10.Querys;
using System.Data;
using System.Data.SqlClient;

namespace HW10.Repository
{
    public class SqlRepository : IRepository
    {
        public void AddUser(User user)
        {
            using (IDbConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                connect.Execute(Query.Create, new {UserName =user.UserName, Password = user.Password,Status = (int)user.Status});
            }
        }

        public List<User> GetAll()
        {
            using (IDbConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                return connect.Query<User>(Query.GetAll).ToList();
            }
        }

        public User GetByName(string username)
        {
            using (IDbConnection connect = new SqlConnection (Connection.ConnectionString))
            {
                return connect.QueryFirstOrDefault<User>(Query.GetByName, new { UserName = username });
            }
        }

        public void UpdateUser(User user)
        {
            using (IDbConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                connect.Execute(Query.UpdateUser, new { Password = user.Password, Status = (int)user.Status, UserName = user.UserName });
            }
        }
    }
}
