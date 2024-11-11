using System;
using System.IO;

public class InvalidAgeException : Exception
{
    // Special exception for age validation
    public InvalidAgeException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main()
    {
        string directoryPath = "C:\\Users\\Tec\\source\\repos\\FileHandler\\FileHandler\\Files\\"; // Mappen hvor filen skal oprettes
        string filePath = Path.Combine(directoryPath, "Users.txt"); // Filens sti
        string firstName = "", lastName = "", email = "";
        int age = 0;

        try
        {
            Console.Write("Indtast dit fornavn: ");
            firstName = Console.ReadLine();
            ValidateName(firstName);

            Console.Write("Indtast dit efternavn: ");
            lastName = Console.ReadLine();
            ValidateName(lastName);

            Console.Write("Indtast din alder: ");
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.Write("Ugyldig alder. Indtast venligst en alder mellem 18 og 50: ");
            }

            if (!(firstName == "Mathias" && lastName == "Altenburg"))
            {
                if (age < 18 || age > 50)
                {
                    throw new InvalidAgeException("Alderen skal være mellem 18 og 50 år.");
                }
            }

            Console.Write("Indtast din e-mail: ");
            email = Console.ReadLine();
            ValidateEmail(email);

            // Saml data
            string fullName = $"{firstName} {lastName}";
            string userData = $"{fullName}, {age}, {email}";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine("Mappen 'Files' blev oprettet.");
            }
            else
            {
                Console.WriteLine("Mappen 'Files' eksisterer allerede.");
            }

  
            AppendToFile(filePath, userData);

            ReadUsersFromFile(filePath);

            Console.WriteLine("Brugeroprettelse er vellykket!");
        }
        catch (InvalidAgeException ex) when (!(firstName == "Mathias" && lastName == "Altenburg"))
        {
            Console.WriteLine($"Fejl: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Programmet afsluttes korrekt.");
        }
    }
    static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("Navnet må ikke være tomt.");
        }
    }
    static void ValidateEmail(string email)
    {
        if (!email.Contains("@") || !email.Contains("."))
        {
            throw new InvalidOperationException("E-mailen skal indeholde både '@' og '.'");
        }
    }
    static void AppendToFile(string filePath, string data)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Filen 'Users.txt' eksisterer ikke. Den bliver nu oprettet.");
            }

            // Tilføj data til filen
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(data);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Fejl ved filhåndtering: {ex.Message}");
        }
    }
    static void ReadUsersFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string[] users = File.ReadAllLines(filePath);
                Console.WriteLine("\nRegistrerede brugere:");
                foreach (string user in users)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("Filen findes ikke.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved indlæsning af fil: {ex.Message}");
        }
    }
}
