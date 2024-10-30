
using HW10.Enums;

namespace HW10.UserService;

public interface IUserService
{
    public void Register(string username, string password);
    public void Login(string username, string password);
    public void UpdateStatus(StatusEnum status);
    public void Search(string username);
    public void ChangePassword(string oldPassword, string newPassword);
    public void Logout();
}
