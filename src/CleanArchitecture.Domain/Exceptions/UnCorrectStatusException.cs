using System;

namespace CleanArchitecture.Domain.Exceptions
{
    public class UnCorrectStatusException : Exception
    {
        public UnCorrectStatusException(string status) : base(
            $"Status should be {status}.")
        {
        }
    }
}