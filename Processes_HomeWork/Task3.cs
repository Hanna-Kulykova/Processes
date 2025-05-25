using System.Diagnostics;

namespace Processes_HomeWork;

public class Task3
{
    public static void Start()
    {
        Console.Write("Введіть перше число: ");
        string num1 = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Введіть друге число: ");
        string num2 = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Операція (+, -, *, /): ");
        string operation = Console.ReadLine()?.Trim() ?? "";
        
        try
        {
            string basePath = AppContext.BaseDirectory;
            string childAppRelativePath = @"../../../../CalculatorProcessApp/bin/Debug/net9.0/CalculatorProcessApp.dll";
            string fullChildAppPath = Path.GetFullPath(Path.Combine(basePath, childAppRelativePath));
            Console.WriteLine($"Запуск: dotnet \"{fullChildAppPath}\" {num1} {num2} {operation}");
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"\"{fullChildAppPath}\" {num1} {num2} {operation}",
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