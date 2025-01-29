using System.Runtime.InteropServices;

namespace Data.Helpers;

// This creates a connectionstring for an SQLite_Database file, and also creates the db_file in the process.
// It does not instantiate the tables, but it creates a file in "Data/Databases", if that fails it's either going to be
// C:\Projects\Database\ or ~/Projects/Database, depending on if you are on Windows or Linux.
// In terminal go to "DataStorage_Assignment/Data" and execute the command "dotnet-ef database update"
// To instantiate the data tables for the database to work.

public class DatabaseHelper
{
    public static string GetDatabaseConnectionString()
    {
        var databasePath = "SQLite_Database.db";
        try
        {
            databasePath = "Databases/SQLite_Database.db";
        }
        catch
        {
            // Determine the correct database path based on the OS
            databasePath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @"C:\Projects\DataBase\SQLite_Database.db"
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Projects", "Database", "SQLite_Database.db");
        }
        EnsureDatabaseExists(databasePath);
        return $"Data Source={databasePath}";
    }

    private static void EnsureDatabaseExists(string databasePath)
    {
        // Ensure the directory exists
        string? directoryPath = Path.GetDirectoryName(databasePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Create the database file if it does not exist
        if (!File.Exists(databasePath))
        {
            File.Create(databasePath).Close(); // Close to release the file handle
        }
    }
}