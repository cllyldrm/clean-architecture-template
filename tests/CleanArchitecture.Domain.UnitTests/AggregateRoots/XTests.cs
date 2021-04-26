using CleanArchitecture.Domain.AggregateRoots;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Domain.UnitTests.AggregateRoots
{
    public class XTests
    {
        [Test]
        public void WhenStatusUnCorrect_ShouldThrowUnsupportedStatusException()
        {
            var aggregateRoot = new XAggregateRoot {Status = Status.Started};

            FluentActions.Invoking(() => aggregateRoot.ValidateStatus(Status.Finished))
                .Should().Throw<UnCorrectStatusException>();
        }
    }
}