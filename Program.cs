using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;
        string sourceDir;
        string targetDir;

        if (args.Length >= 2)
        {
            sourceDir = args[0];
            targetDir = args[1];
        }
        else
        {
            Console.WriteLine("Введіть шлях до вихідної директорії:");
            sourceDir = Console.ReadLine();

            Console.WriteLine("Введіть шлях до цільової директорії:");
            targetDir = Console.ReadLine();
        }

        if (!Directory.Exists(sourceDir))
        {
            Console.WriteLine("Помилка: Вказана вихідна директорія не існує!");
            Environment.ExitCode = 1;
            Console.WriteLine("Програма завершила роботу з кодом 1");
            return;
        }

        if (!Directory.Exists(targetDir))
        {
            Console.WriteLine("Помилка: Вказана цільова директорія не існує!");
            Environment.ExitCode = 1;
            Console.WriteLine("Програма завершила роботу з кодом 1");
            return;
        }

        Console.WriteLine("Введіть шаблон для пошуку файлів (наприклад, *.exe або *.txt):");
        string searchPattern = Console.ReadLine();

        try
        {
            string[] files = Directory.GetFiles(sourceDir, searchPattern, SearchOption.AllDirectories);
            long totalSize = 0;

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                Console.WriteLine($"Файл: {fileInfo.FullName}, Розмір: {fileInfo.Length} байт");

                if ((fileInfo.Attributes & FileAttributes.Hidden) != 0)
                {
                    Console.WriteLine($"Файл {fileInfo.Name} є прихованим");
                }
                if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                {
                    Console.WriteLine($"Файл {fileInfo.Name} доступний тільки для читання");
                }
                if ((fileInfo.Attributes & FileAttributes.Archive) == 0)
                {
                    Console.WriteLine($"Файл {fileInfo.Name} не має атрибут архівації");
                }

                totalSize += fileInfo.Length;
            }

            Console.WriteLine($"Загальний обсяг файлів у вихідній директорії: {totalSize} байт");
            Environment.ExitCode = 0;
            Console.WriteLine("Програма завершила роботу з кодом 0");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            Environment.ExitCode = 1;
            Console.WriteLine("Програма завершила роботу з кодом 1");
        }
    }
}

