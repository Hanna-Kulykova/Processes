using System.Text.RegularExpressions;

namespace WordSearchProcessApp;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Будь ласка, передайте шлях до файлу та слово для пошуку як аргументи.");
            return;
        }

        string filePath = args[0];
        string searchWord = args[1];

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл не знайдено: {filePath}");
            return;
        }

        try
        {
            string content = File.ReadAllText(filePath);
            
            int count = Regex.Matches(content, $@"\b{Regex.Escape(searchWord)}\b", RegexOptions.IgnoreCase).Count;

            Console.WriteLine($"Слово '{searchWord}' зустрічається у файлі {count} разів.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при читанні файлу: {ex.Message}");
        }
    }
}