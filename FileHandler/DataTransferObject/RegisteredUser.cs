using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler.DataTransferObject
{
    public class RegisteredUser : IComparable<RegisteredUser>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public RegisteredUser(string firstName, string lastName, int age, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
        }

        // Implementer IComparable til sortering efter efternavn
        public int CompareTo(RegisteredUser other)
        {
            if (other == null) return 1;
            return LastName.CompareTo(other.LastName);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age}, {Email}";
        }
    }
}
