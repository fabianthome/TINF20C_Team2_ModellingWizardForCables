namespace CableWizardBackend;

public static class Logger
{
    private static readonly string Filepath;

    static Logger()
    {
        // todo
        Filepath = $"";
    }

    public static void Info(string message)
    {
        Log(message, "Info");
    }

    public static void Warning(string message)
    {
        Log(message, "Warning");
    }

    public static void Error(string message)
    {
        Log(message, "Error");
    }

    public static void Fatal(string message)
    {
        Log(message, "Fatal");
    }

    private static void Log(string message, string reason)
    {
        var date = CurrentDateTime();
        var line = $"{date} {reason}: {message}";

        Console.WriteLine(line);

        using var streamWriter = new StreamWriter(Filepath);
        streamWriter.WriteLine(line);
        streamWriter.Close();
    }

    private static string CurrentDateTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}