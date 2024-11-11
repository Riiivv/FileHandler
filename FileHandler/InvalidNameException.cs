using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler;

public class InvalidNameException : Exception
{
    public InvalidNameException(string message) : base(message) { }
}
public class InvalidAgeException : Exception
{
    // Special exception for age validation
    public InvalidAgeException(string message) : base(message) { }
}


public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message, string innerMessage) : base(message, new Exception(innerMessage)) { }
}

