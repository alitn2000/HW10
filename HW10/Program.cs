using HW10.Command_Controller;

CommandController commandController = new CommandController();

while (true)
{
    Console.Write("-> ");
    string command = Console.ReadLine();

    if (string.IsNullOrEmpty(command))
    {
        Console.WriteLine("No command entered. Exiting...");
        break;
    }

    try
    {
        commandController.CommandAnalaysor(command);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}