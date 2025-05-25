using System.Diagnostics;

namespace Processes_HomeWork;

class Program
{
    static void Main(string[] args)
    {
        /* Task 1 */
        //StartCalculator();
        
        /* Task 2 */
        Console.WriteLine("If you want to start Calculator: write \"c\"\n" +
                          "If you want to start Notes: write \"n\"\n" +
                          "If you want to start Freeform: write \"f\"\n");
        var choice = Console.ReadLine();
        if (choice == "c")
            StartCalculator();
        else if (choice == "n") 
            StartNotes();
        else if (choice == "f") 
            StartFreeform();
        else 
            Console.WriteLine("Invalid choice");
    }

    private static void StartFreeform()
    {
        try
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "open";
            proc.StartInfo.Arguments = "-a Freeform";
            proc.Start();
            Console.WriteLine($"Запущено процес: {proc.StartInfo.Arguments.Split(' ')[1]}, очікування на завершення...");
            proc.WaitForExit();

            while (IsProcessRunningMac("Freeform"))
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

    private static void StartNotes()
    {
        try
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "open";
            proc.StartInfo.Arguments = "-a Notes";
            proc.Start();
            Console.WriteLine($"Запущено процес: {proc.StartInfo.Arguments.Split(' ')[1]}, очікування на завершення...");
            proc.WaitForExit();

            int pid = GetPidOfApp(appPath);
            if (pid == -1)
            {
                Console.WriteLine("Не вдалося знайти PID додатку.");
                return;
            }

            Console.WriteLine($"PID процесу: {pid}, очікування завершення...");

            while (IsProcessRunningByPid(pid))
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

    public static void StartCalculator()
    {
        try
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "open";
            proc.StartInfo.Arguments = "-a Calculator";
            proc.Start();
            Console.WriteLine($"Запущено процес: {proc.StartInfo.Arguments.Split(' ')[1]}, очікування на завершення...");
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
    
    public static bool IsProcessRunningMac(int pid)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = $"-c \"ps -p {pid} > /dev/null\"",
            RedirectStandardOutput = false,
            RedirectStandardError = false,
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
            Console.WriteLine($"Помилка при перевірці PID {pid}: {e.Message}");
            return false;
        }
    }
}
