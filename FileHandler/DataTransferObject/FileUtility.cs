using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler.DataTransferObject
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    namespace FileHandler
    {
        public static class FileUtility
        {
            private static readonly string directoryPath = "C:\\Users\\alten\\Source\\Repos\\FileHandler\\FileHandler\\Files\\";
            private static readonly string filePath = Path.Combine(directoryPath, "Users.txt");

            // Metode til at gemme en bruger i filen
            public static void SaveUserToFile(RegisteredUser user)
            {
                string message = string.Empty;
                try
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine($"{user.FirstName},{user.LastName},{user.Age},{user.Email}");
                    }
                }
                catch (IOException ex)
                {
                   message = ($"Fejl ved filhåndtering: {ex.Message}");
                }
            }

            // Metode til at læse brugere fra filen
            public static List<RegisteredUser> LoadUsersFromFile()
            {
                string message = string.Empty;
                var users = new List<RegisteredUser>();
                try
                {
                    if (File.Exists(filePath))
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        foreach (string line in lines)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 4)
                            {
                                string firstName = parts[0].Trim();
                                string lastName = parts[1].Trim();
                                int age = int.Parse(parts[2].Trim());
                                string email = parts[3].Trim();
                                users.Add(new RegisteredUser(firstName, lastName, age, email));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = ($"Fejl ved indlæsning af fil: {ex.Message}");
                }
                return users;
            }

            // Metode til udskrivning af brugere
            public static void PrintUsers(List<RegisteredUser> users)
            {
                string message = string.Empty;
                if (users.Count > 0)
                {
                   message = ("\nRegistrerede brugere:");
                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }
                }
                else
                {
                    message = ("Ingen brugere fundet.");
                }
            }
        }
    }

}
