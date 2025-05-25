using System.Diagnostics;

namespace Processes_HomeWork;

public class Task1
{
    public static void Start()
    {
        try
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "open";
            proc.StartInfo.Arguments = "-a Calculator";
            proc.Start();
            Console.WriteLine("Калькулятор запущено. Очікування на його завершення...");
            
            while (IsProcessRunning("Calculator"))
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Калькулятор завершив роботу.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
    }

    private static bool IsProcessRunning(string processName)
    {
        try
        {
            var ps = new Process();
            ps.StartInfo.FileName = "pgrep";
            ps.StartInfo.Arguments = processName;
            ps.StartInfo.RedirectStandardOutput = true;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.CreateNoWindow = true;
            ps.Start();

            string output = ps.StandardOutput.ReadToEnd();
            ps.WaitForExit();

            return !string.IsNullOrEmpty(output);
        }
        catch
        {
            return false;
        }
    }
}