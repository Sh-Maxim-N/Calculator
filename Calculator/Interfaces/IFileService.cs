namespace Calculator.Interfaces;

public interface IFileService
{
    public bool FileExist(string inputpath);
    public string[]? GetStringsFromFile(string path);
    public void WriteLines(string outputPath, List<string> result);
}