

namespace HW10.Command_Controller;

public interface ICommandController
{
    public void CommandAnalaysor(string command);
    public string GetArgument(string[] parts, string key);
}
