namespace HW10.Querys;

public static class Query
{
    public static string Create = "insert into Users (UserName , Password , Status) values (@UserName, @Password, @Status)";
    public static string GetByName = "select * from Users where UserName = @UserName";
    public static string GetAll = "select * from Users";
    public static string UpdateUser = "update Users Set Status = @Status , Password = @Password where UserName = @UserName";

}
