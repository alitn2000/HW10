

using HW10.ENTITIES;
using HW10.Enums;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.Json.Nodes;

namespace HW10.Repository;

public class FileRepository : IRepository
{
    private string Path = $"UserDb.json";

    public void AddUser(User user)
    {
        if (user.ValidPass(user.Password))
        {
            var users = GetAll() ?? new List<User>();
            users.Add(user);
            File.WriteAllText(Path, JsonConvert.SerializeObject(users, Formatting.Indented));
        }
       
    }



    public List<User> GetAll()
    {
        if (!File.Exists(Path)) return new List<User>();

        var data = File.ReadAllText(Path);
        var users = JsonConvert.DeserializeObject<List<User>>(data);
        return users ?? new List<User>();
    }

    public User GetByName(string username)
    {
        var users = GetAll();

        var existUser = users.FirstOrDefault(u => u.UserName == username);
        return existUser;
    }

    public void UpdateUser(User user)
    {
        var data = File.ReadAllText(Path);
        var users = JsonConvert.DeserializeObject<List<User>>(data);

        var existingUser = users.FirstOrDefault(u => u.UserName == user.UserName);
        if (existingUser == null)
        {
            throw new Exception("User not found in the system.");
        }

        existingUser.Password =user.Password;
        existingUser.Status = user.Status;

        var updatedData = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(Path, updatedData);

        Console.WriteLine("User information updated successfully.");
    }
}
