using System.Diagnostics;

namespace Processes_HomeWork;

class Program
{
    static void Main(string[] args)
    {
        /* Task 1 */
        StartCalculator();
    }
    
    public static void StartCalculator()
    {
        try
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "open";
            proc.StartInfo.Arguments = "-a Calculator";
            proc.Start();
            Console.WriteLine($"Запущено процес: {proc.StartInfo.FileName}, очікування на завершення...");
            proc.WaitForExit();
            
            while (IsProcessRunning("Calculator"))
            {
                Thread.Sleep(1000);
            }
            
            int exitCode = proc.ExitCode;
            Console.WriteLine($"Процес завершився з кодом: {exitCode}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public static bool IsProcessRunning(string processName)
    {
        try
        {
            var ps = new Process();
            ps.StartInfo.FileName = "pgrep";
            ps.StartInfo.Arguments = $"-x {processName}";
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
