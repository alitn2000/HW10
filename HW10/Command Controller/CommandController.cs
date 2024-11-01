﻿
using HW10.ENTITIES;
using HW10.Enums;
using HW10.Repository;
using HW10.UserService;

namespace HW10.Command_Controller;

public class CommandController : ICommandController
{
    private FileRepository _fileRepository = new FileRepository();
    private HW10.UserService.UserService _userService = new HW10.UserService.UserService();

    public void CommandAnalaysor(string command)
    {
        var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var instructor = parts[0].ToLower();

        try
        {
            var statusDic = new Dictionary<string, StatusEnum>
            {
                { "available", StatusEnum.available },
                { "not available", StatusEnum.notavailable }
            };

            switch (instructor)
            {
                case "register":
                    var username = GetArgument(parts, "--username");
                    var password = GetArgument(parts, "--password");
                    _userService.Register(username, password);
                    break;

                case "login":
                    username = GetArgument(parts, "--username");
                    password = GetArgument(parts, "--password");
                    _userService.Login(username, password);
                    break;

                case "change":
                    if (parts.Length < 3) 
                    {
                        Console.WriteLine("Missing arguments for change command.");
                        return;
                    }

                    var statusValue = GetArgument(parts, "--status").ToLower();

                    if (statusDic.TryGetValue(statusValue, out var statusEnum))
                    {
                        _userService.UpdateStatus(statusEnum);
                        Console.WriteLine($"Status changed to {statusValue}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid status. Please use 'available' or 'not available'.");
                    }
                    break;

                case "changepassword":
                    var oldPassword = GetArgument(parts, "--old");
                    var newPassword = GetArgument(parts, "--new");
                    _userService.ChangePassword(oldPassword, newPassword);
                    break;

                case "logout":
                    _userService.Logout();
                    break;

                case "search":
                    var userName = GetArgument(parts, "--username");
                    _userService.Search(userName);
                    break;

                default:
                    throw new Exception("Invalid command!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public string GetArgument(string[] parts, string key)
    {
        int index = Array.IndexOf(parts, key);
        if (index == -1 || index + 1 >= parts.Length)
            throw new ArgumentException($"Missing value for {key}");
        //return parts[index+1]

        var value = new List<string>();
        for (int i = index + 1; i < parts.Length; i++)
        {
            if (parts[i].StartsWith("--")) 
                break;
            value.Add(parts[i]);
        }

        return string.Join(" ", value);
    }
}