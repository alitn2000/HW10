

using HW10.ENTITIES;
using HW10.Enums;
using HW10.Exceptions;
using HW10.Repository;

namespace HW10.UserService;

public class UserService : IUserService
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly FileRepository fileRepo = new FileRepository();
    public User _currentUser;

    public void Register(string username, string password)
    {

        var existingUser = fileRepo.GetByName(username);
        if (existingUser != null)
        {
            Console.WriteLine("Register failed! Username already exists.");
            return;
        }
        else
        {
            fileRepo.AddUser(new User(username, password));
            Console.WriteLine("User registered successfully.");
        }



    }

    public void Login(string username, string password)
    {
        var user = _ = fileRepo.GetByName(username);
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
        fileRepo.UpdateUser(_currentUser);
    }

    public void Search(string username)
    {
        var users = fileRepo.GetAll();
        var results = users.FindAll(u => u.UserName.StartsWith(username, StringComparison.OrdinalIgnoreCase));

        foreach (var user in results)
        {
            Console.WriteLine($"{user.UserName} | Status: {user.Status}");
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
                _currentUser.ChangePass(oldPassword, newPassword);

                fileRepo.UpdateUser(_currentUser);

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
