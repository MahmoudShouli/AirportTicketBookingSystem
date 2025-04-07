namespace AirportTicketBookingSystem.Services;

public static class FileServices
{
    public static void SaveImportedFile(string sourcePath)
    {
        try
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            var dataDirectory = Path.Combine(projectRoot, "Data");
            
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            
            var destinationPath = Path.Combine(dataDirectory, "Flights.csv");

            
            File.Copy(sourcePath, destinationPath, overwrite: true);

            Console.WriteLine("File imported successfully to the Data directory");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error importing file: " + ex.Message);
        }
        
    }

}