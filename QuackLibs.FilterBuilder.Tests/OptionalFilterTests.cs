using FluentAssertions;

namespace QuackLibs.FilterBuilder.Tests;
public class OptionalFilterTests
{
    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnOptionalilterThatEvaluatesToTrue_OneResultsIsReturned()
    {
        //arrange
        var optionalFilterValue = new List<PersonStud> { new PersonStud { Name = "testje" } };

        var filter = FilterBuilder.For<PersonStud>(true)
                                  .When(() => true)
                                    .Then(e => e.Name.Contains("testje"));

        var result = optionalFilterValue.Where(filter);

        result.Should().NotBeEmpty();
        result.Count().Should().Be(1);
    }

    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnOptionalFilterThatEvaluatesToFalse_NoResultsAreFound()
    {
        //arrange
        var optionalFilterValue = new PersonStud { Name = "optional" };
        var andFiltervalue = new PersonStud { Name = "andFilter" };

        //act
        var filter = FilterBuilder.For<PersonStud>(false)
                                  .When(() => false)
                                    .Then(e => e.Name.Contains("optional"))
                                  .And(e => e.FirstName == "andFilter");


        var result = new List<PersonStud> { optionalFilterValue, andFiltervalue }.Where(filter);

        //assert
        result.Should().BeEmpty();
        result.Count().Should().Be(0);
    }


    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnOptionalFilterThatEvaluatesToFalse_TheAndFiltersIsExecuted()
    {
        //arrange
        var optionalFilterValue = new PersonStud { Name = "optional" };
        var andFiltervalue = new PersonStud { Name = "andFilter" };

        Filter<PersonStud> filter = FilterBuilder.For<PersonStud>(defaultFilter: false)
                                                 .When(() => false)
                                                   .Then(e => e.Name.Contains("optional"))
                                                 .And(e => e.Name.Equals("andFilter"));

        var result = new List<PersonStud> { optionalFilterValue, andFiltervalue }.Where(filter);

        result.Should().NotBeEmpty();
        result.Count().Should().Be(1);
    }
}


