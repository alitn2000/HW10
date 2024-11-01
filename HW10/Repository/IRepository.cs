
using HW10.ENTITIES;
using HW10.Enums;

namespace HW10.Repository;

public interface IRepository
{
    void AddUser(User user);
    List<User> GetAll();
    User GetByName(string username);
    public void UpdateUser(User user);
}
