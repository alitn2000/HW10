
using HW10.Enums;
using HW10.Exceptions;

namespace HW10.ENTITIES;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public StatusEnum Status { get; set; }
    public User()
    {
        
    }
    public User(string username, string pass)
    {
        UserName = username;
        Password = pass;
        Status = StatusEnum.notavailable;
    }
    public void SetPass(string pass)
    {
        if (ValidPass(pass))
        {
            Password = pass;
        }
        else
        {
            Console.WriteLine("Password is Invalid!!!");
        }
    }

    public bool ValidPass(string pass)
    {
        char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '/', '\\', '<', '>', '{', '}', '[', ']', '|', '`' };

        if (string.IsNullOrWhiteSpace(pass) || pass.Length < 8 || !pass.Any(char.IsUpper) || !pass.Any(char.IsLower) || !pass.Any(char.IsDigit))
        {
            throw new PassError();
        }

        bool containsSpecialChar = false;
        foreach (char c in pass)
        {
            if (specialCharacters.Contains(c))
            {
                containsSpecialChar = true;
                break;
            }
        }

        if (!containsSpecialChar)
        {
            return false;
        }

        return true;

    }

   
   
}

