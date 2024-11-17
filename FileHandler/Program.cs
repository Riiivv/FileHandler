using FileHandler.DataTransferObject.FileHandler;
using FileHandler.DataTransferObject;
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
        List<RegisteredUser> users = FileUtility.LoadUsersFromFile();

        string firstName, lastName, email;
        int age;

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

            if (!(firstName == "Mathias" && lastName == "Altenburg") && (age < 18 || age > 50))
            {
                throw new InvalidAgeException("Alderen skal være mellem 18 og 50 år.");
            }

            Console.Write("Indtast din e-mail: ");
            email = Console.ReadLine();
            ValidateEmail(email);

            var newUser = new RegisteredUser(firstName, lastName, age, email);
            users.Add(newUser);

            // Gem den nye bruger i filen
            FileUtility.SaveUserToFile(newUser);

            // Sorter brugerne efter efternavn
            users.Sort();

            // Udskriv brugerne
            FileUtility.PrintUsers(users);
        }
        catch (InvalidAgeException ex)
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
}
