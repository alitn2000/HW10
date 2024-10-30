﻿
using HW10.Enums;
using HW10.Exceptions;

namespace HW10.ENTITIES;

public class User
{
    public string UserName { get; set; }
    public string Password { get; protected set; }
    public StatusEnum Status { get; set; }

    public User(string username, string pass)
    {
        UserName = username;
        Password = pass;
        Status = StatusEnum.notavailble;
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

    public void ChangePass(string oldPassword, string newPassword)
    {
        if (Password == oldPassword)
        {
            SetPass(newPassword);
        }
        else
        {
            throw new PassError("Incorrect old password!!!");
        }

    }

   
}
