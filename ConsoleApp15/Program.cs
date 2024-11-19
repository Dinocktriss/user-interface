using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class User
{
    // Свойства пользователя
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Конструктор по умолчанию
    public User() { }

    // Конструктор с параметрами
    public User(int userId, string username, string email, string password)
    {
        UserId = userId;
        Username = username;
        Email = email;
        Password = password;
    }

    // Метод для отображения информации о пользователе
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {UserId}, Username: {Username}, Email: {Email}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        int currentUserId = 1;
        string command = string.Empty;
        List<User> users = new List<User>();

        while (true)
        {
            Console.WriteLine("Введите команду (new для создания нового пользователя, login для входа, exit для выхода):");
            command = Console.ReadLine();

            if (command.ToLower() == "exit")
            {
                break;
            }
            else if (command.ToLower() == "new")
            {
                string username;
                while (true)
                {
                    Console.WriteLine("Введите имя пользователя:");
                    username = Console.ReadLine();
                    if (!users.Any(u => u.Username == username))
                    {
                        break;
                    }
                    Console.WriteLine("Имя пользователя уже занято. Попробуйте снова.");
                }

                string email;
                while (true)
                {
                    Console.WriteLine("Введите email пользователя:");
                    email = Console.ReadLine();
                    if (IsValidEmail(email))
                    {
                        break;
                    }
                    Console.WriteLine("Email невалиден. Попробуйте снова.");
                }

                string password;
                string confirmPassword;
                while (true)
                {
                    Console.WriteLine("Введите пароль пользователя:");
                    password = ReadPassword();

                    Console.WriteLine("Подтвердите пароль:");
                    confirmPassword = ReadPassword();

                    if (password == confirmPassword)
                    {
                        break;
                    }
                    Console.WriteLine("Пароли не совпадают. Попробуйте снова.");
                }

                // Создание экземпляра пользователя с введенными данными и уникальным ID
                User user = new User(currentUserId, username, email, password);
                users.Add(user);

                // Пример использования метода для отображения информации о пользователе
                user.DisplayInfo();

                // Увеличение ID для следующего пользователя
                currentUserId++;
            }
            else if (command.ToLower() == "login")
            {
                Console.WriteLine("Введите имя пользователя:");
                string username = Console.ReadLine();

                Console.WriteLine("Введите пароль:");
                string password = ReadPassword();

                User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    Console.WriteLine("Неверное имя пользователя или пароль. Попробуйте снова.");
                }
                else
                {
                    Console.WriteLine($"Добро пожаловать, {user.Username}!");

                    while (true)
                    {
                        Console.WriteLine("Введите уравнение в формате (a + b), (a - b), (a * b), (a / b) или введите logout для выхода из учетной записи:");
                        string equation = Console.ReadLine();

                        if (equation.ToLower() == "logout")
                        {
                            break;
                        }
                        else
                        {
                            double result = SolveEquation(equation);
                            Console.WriteLine($"Результат: {result}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Неизвестная команда. Попробуйте снова.");
            }
        }
    }

    private static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".") && email.IndexOf("@") < email.LastIndexOf(".");
    }

    private static string ReadPassword()
    {
        StringBuilder password = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) break;
            password.Append(key.KeyChar);
        }
        Console.WriteLine();
        return password.ToString();
    }

    private static double SolveEquation(string equation)
    {
        var parts = equation.Split(' ');
        double a = Convert.ToDouble(parts[0]);
        char op = parts[1][0];
        double b = Convert.ToDouble(parts[2]);

        return op switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => a / b,
            _ => throw new InvalidOperationException("Неподдерживаемый оператор.")
        };
    }
}
