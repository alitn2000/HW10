

namespace HW10.ConnectionString;

public static class Connection
{
    public static string ConnectionString { get; set; }

    static Connection()
    {
        ConnectionString = @"Data Source=DESKTOP-URA992G\SQLEXPRESS; Initial Catalog=HW10; Integrated Security=true ;TrustServerCertificate=True;";
        
    }
}
