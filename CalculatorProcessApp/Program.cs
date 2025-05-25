namespace CalculatorProcessApp;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Очікується 3 аргументи: число1, число2, операція (+ - * /)");
            return;
        }

        if (!double.TryParse(args[0], out double num1) || !double.TryParse(args[1], out double num2))
        {
            Console.WriteLine("Аргументи 1 і 2 мають бути числами.");
            return;
        }

        string operation = args[2];
        double result;
        bool validOperation = true;

        switch (operation)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 == 0)
                {
                    Console.WriteLine("Ділення на нуль неможливе.");
                    return;
                }
                result = num1 / num2;
                break;
            default:
                Console.WriteLine($"Невідома операція: '{operation}'. Дозволені: + - * /");
                validOperation = false;
                result = 0;
                break;
        }

        if (validOperation)
        {
            Console.WriteLine($"Аргументи: {num1}, {num2}, '{operation}'");
            Console.WriteLine($"Результат: {result}");
        }
    }
}