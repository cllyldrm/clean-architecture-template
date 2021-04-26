using System;

namespace CleanArchitecture.Domain.Exceptions
{
    public class UnsupportedStatusException : Exception
    {
        public UnsupportedStatusException(string status) : base(
            $"Status \"{status}\" is unsupported.")
        {
        }
    }
}