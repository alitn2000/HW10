
namespace HW10.Exceptions;

public class PassError : Exception
{
    public PassError(string message) : base(message) { }
    public PassError() : base("The password is not Invalid !!!") { }

}
