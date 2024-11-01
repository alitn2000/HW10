

using HW10.ENTITIES;
using HW10.Enums;
using HW10.Exceptions;
using HW10.Repository;

namespace HW10.UserService;

public class UserService : IUserService
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();
    //private readonly FileRepository fileRepo = new FileRepository();
    //private readonly IRepository Sql = new SqlRepository();
    private readonly IRepository SqlADO = new SqlRepository_ADO();

    public User _currentUser;

    public void Register(string username, string password)
    {

        //var existingUser = fileRepo.GetByName(username);
        //var existingUser = Sql.GetByName(username);
        var existingUser = SqlADO.GetByName(username);
        if (existingUser != null)
        {
            Console.WriteLine("Register failed! Username already exists.");
            return;
        }
        else
        {
            //fileRepo.AddUser(new User(username, password));
            //Sql.AddUser(new User(username, password));
            SqlADO.AddUser(new User(username, password));
            Console.WriteLine("User registered successfully.");
        }



    }

    public void Login(string username, string password)
    {
        // var user = fileRepo.GetByName(username);
        //var user = Sql.GetByName(username);
        var user = SqlADO.GetByName(username);
        if (user.Password != password)
        {
            throw new Exception("Incorrect password!!!");
        }
        else
        {
            _currentUser = user;
            Console.WriteLine("User logged in successfully.");
        }
    }

    public void UpdateStatus(StatusEnum status)
    {
        if (_currentUser == null)
            throw new Exception("User not logged in.");

        _currentUser.Status = status;
        //fileRepo.UpdateUser(_currentUser);
        //Sql.UpdateUser(_currentUser);
        SqlADO.UpdateUser(_currentUser);
    }

    public void Search(string username)
    {
        if (_currentUser == null)
            throw new Exception("User not logged in.");
        //var users = fileRepo.GetAll();
        //var users = Sql.GetAll();
        var users = SqlADO.GetAll();
        var results = users.FindAll(u => u.UserName.StartsWith(username));
        if (results.Count() == 0)
        {
            throw new Exception("no user found !!!");
        }
        else
        {
            foreach (var user in results)
            {
                Console.WriteLine($"{user.UserName} | Status: {user.Status}");
            }
        }
       
    }

    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (_currentUser == null)
            throw new Exception("User not logged in.");
        if (_currentUser.ValidPass(newPassword))
        {
            try
            {

                if (_currentUser.Password == oldPassword)
                {
                    _currentUser.Password = newPassword;
                }
                else
                {
                    throw new PassError("Incorrect old password!!!");
                }

                //fileRepo.UpdateUser(_currentUser);
                //Sql.UpdateUser(_currentUser);
                SqlADO.UpdateUser(_currentUser);

                Console.WriteLine("Password changed successfully.");
            }
            catch (PassError ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ٍError");
            }
        }
        else
        {
            throw new PassError();
        }
       
    }

    public void Logout()
    {
        if (_currentUser is not null)
        {
            _currentUser = null;
            Console.WriteLine("User logged out successfully.");
        }
        else
        {
            throw new Exception("no user loged in!!!");
        }
    }


}
