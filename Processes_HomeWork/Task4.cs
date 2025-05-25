using System.Diagnostics;

namespace Processes_HomeWork;

public class Task4
{
    public static void Start()
    {
        Console.Write("Введіть шлях до файлу: ");
        string filePath = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Введіть слово для пошуку: ");
        string searchWord = Console.ReadLine()?.Trim() ?? "";

        try
        {
            string basePath = AppContext.BaseDirectory;
            string childAppRelativePath = @"../../../../WordSearchProcessApp/bin/Debug/net9.0/WordSearchProcessApp.dll";
            string fullChildAppPath = Path.GetFullPath(Path.Combine(basePath, childAppRelativePath));

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"\"{fullChildAppPath}\" \"{filePath}\" \"{searchWord}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();

            proc.WaitForExit();

            Console.WriteLine("Вивід дочірнього процесу:");
            Console.WriteLine(output);

            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine("Помилка:");
                Console.WriteLine(error);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка запуску дочірнього процесу: " + e.Message);
        }
    }
}