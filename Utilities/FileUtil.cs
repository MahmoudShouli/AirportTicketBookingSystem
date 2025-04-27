namespace AirportTicketBookingSystem.Utilities;

public static class FileUtil
{
    public static List<T> ConvertFileToList<T>(string path, Func<string[], T> mapFunc)
    {
        try
        {
            var lines = File.ReadAllLines(path);

            return lines
                .Skip(1) 
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return mapFunc(parts);
                })
                .ToList();
        }
        catch (Exception)
        {
            return new List<T>();
        }
    }
    
    public static void SaveImportedFile(string sourcePath, string fileName)
    {
        
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
        var dataDirectory = Path.Combine(projectRoot, "Data");
        
        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }
        
        var destinationPath = Path.Combine(dataDirectory, fileName);
        
        File.Copy(sourcePath, destinationPath, overwrite: true);
    }
}