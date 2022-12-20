
static class Utils
{
    public static string ReadFile(string input = "input.txt")
    {

        var enviroment = Environment.CurrentDirectory;
        string? projectDirectory = Directory.GetParent(enviroment)?.Parent?.FullName;
        string text = null;
        try
        {
            text = File.ReadAllText($"{projectDirectory}/../{input}");
        }
        catch (FileNotFoundException ex)
        {
            // when running from cli dotnet run
            text = File.ReadAllText($"./{input}");
        }
        return text;
    }
}
