using FluentAssertions;

namespace QuackLibs.FilterBuilder.Tests;
public class OptionalFilterTests
{
    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnOptionalilterThatEvaluatesToTrue_OneResultsIsReturned()
    {
        //arrange
        var testPeople = new List<PeopleStud> { new PeopleStud { Name = "testje" } };
        var filter = FilterBuilder.For<PeopleStud>(e => true)
                                  .When(() => true)
                                    .Then(e => e.Name.Contains("testje"));

        var result = testPeople.Where(filter.Compile());

        result.Should().NotBeEmpty();
        result.Count().Should().Be(1);
    }

    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnOptionalFilterThatEvaluatesToFalse_NoResultsAreFound()
    {
        //arrange
        var testPeople = new List<PeopleStud> { new PeopleStud { Name = "testje" } };
        var filter = FilterBuilder.For<PeopleStud>(e => false)
                                  .When(() => false)
                                    .Then(e => e.Name.Contains("testje"));

        var result = testPeople.Where(filter.Compile());

        result.Should().BeEmpty();
        result.Count().Should().Be(0);
    }
}
