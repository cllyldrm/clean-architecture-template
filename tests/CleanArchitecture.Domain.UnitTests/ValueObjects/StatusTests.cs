using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Domain.UnitTests.ValueObjects
{
    public class StatusTests
    {
        [Test]
        public void ShouldReturnCorrectStatus()
        {
            var value = "FINISHED";
            var status = Status.From(value);
            status.Value.Should().Be(value);
        }

        [Test]
        public void ToStringReturnsStatus()
        {
            var status = Status.Finished;
            status.ToString().Should().Be(status.Value);
        }

        [Test]
        public void ShouldPerformImplicitConversionToStatusString()
        {
            string status = Status.Finished;
            status.Should().Be("FINISHED");
        }

        [Test]
        public void ShouldPerformExplicitConversionGivenSupportedStatus()
        {
            var status = (Status) "FINISHED";
            status.Should().Be(Status.Finished);
        }

        [Test]
        public void ShouldThrowUnsupportedStatusExceptionGivenNotSupportedStatus()
        {
            FluentActions.Invoking(() => Status.From("XX"))
                .Should().Throw<UnsupportedStatusException>();
        }
    }
}