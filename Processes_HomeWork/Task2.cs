using System.Diagnostics;

namespace Processes_HomeWork;

public static class Task2
{
    public static void Start()
    {
        Console.WriteLine("If you want to start Calculator: write \"c\"");
        Console.WriteLine("If you want to start Notes: write \"n\"");
        Console.WriteLine("If you want to start Freeform: write \"f\"");
        Console.Write("Your choice: ");
        
        string? input = Console.ReadLine();

        string? appPath = input switch
        {
            "c" => "/System/Applications/Calculator.app",
            "n" => "/System/Applications/Notes.app",
            "f" => "/System/Applications/Freeform.app",
            _ => null
        };

        if (appPath == null)
        {
            Console.WriteLine("Невірний вибір.");
            return;
        }

        StartAppAndTrackByPid(appPath);
    }

    private static void StartAppAndTrackByPid(string appPath)
    {
        try
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "open",
                    Arguments = $"-a \"{appPath}\"",
                    UseShellExecute = false
                }
            };

            proc.Start();
            proc.WaitForExit();

            string appName = Path.GetFileNameWithoutExtension(appPath);
            int pid = GetPidOfApp(appName);

            if (pid == -1)
            {
                Console.WriteLine("Не вдалося отримати PID додатку.");
                return;
            }

            Console.WriteLine($"Запущено процес: {appName}, PID: {pid}. Очікування завершення...");
            Console.WriteLine("Якщо це Notes або Freeform — закрий додаток повністю через Dock (Quit).");

            while (IsProcessRunningByPid(pid))
            {
                Thread.Sleep(1000);
            }
            int exitCode = proc.ExitCode;
            Console.WriteLine($"Процес завершено з кодом {exitCode}.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка запуску: {e.Message}");
        }
    }

    private static int GetPidOfApp(string appName)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = $"-c \"pgrep -x '{appName}'\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            if (int.TryParse(output.Split('\n').FirstOrDefault(), out int pid))
                return pid;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка отримання PID: {e.Message}");
        }

        return -1;
    }

    private static bool IsProcessRunningByPid(int pid)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = $"-c \"ps -p {pid} > /dev/null\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using var process = Process.Start(psi);
            process.WaitForExit();
            return process.ExitCode == 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка перевірки PID: {e.Message}");
            return false;
        }
    }
}