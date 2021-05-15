using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Seedwork;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class Status : ValueObject
    {
        static Status()
        {
        }

        private Status()
        {
        }

        private Status(string status)
        {
            Value = status;
        }

        public string Value { get; private set; }

        public static Status From(string value)
        {
            var status = new Status {Value = value};

            if (!SupportedStatuses.Contains(status))
            {
                throw new UnsupportedStatusException(status);
            }

            return status;
        }

        public static Status Started => new Status("STARTED");

        public static Status Finished => new Status("FINISHED");

        public static implicit operator string(Status status)
        {
            return status.ToString();
        }

        public static explicit operator Status(string status)
        {
            return From(status);
        }

        public override string ToString()
        {
            return Value;
        }

        protected static IEnumerable<Status> SupportedStatuses
        {
            get
            {
                yield return Started;
                yield return Finished;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}