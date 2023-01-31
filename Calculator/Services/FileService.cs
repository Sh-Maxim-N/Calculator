using Calculator.Interfaces;

namespace Calculator.Services;

public class FileService : IFileService
{
    public bool FileExist(string inputpath)
    {
        return File.Exists(inputpath);
    }

    public string[]? GetStringsFromFile(string path)
    {
        if (path == null)
        {
            throw new ArgumentNullException("path");
        }

        string[]? stringArray = File.ReadAllLines(path);

        return stringArray;
    }

    public void WriteLines(string outputPath, List<string> result)
    {
        File.WriteAllLines(outputPath, result);
    }
}