using FluentAssertions;
using System.Linq.Expressions;

namespace QuackLibs.FilterBuilder.Tests;

public class ExtendFilterBuilderTests
{
    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnAndFilterThatEvaluatesToTrue_OneResultsIsReturned()
    {
        //arrange
        var testPeople = new List<PersonStud> { new PersonStud { Name = "testje" } };
        Filter<PersonStud> filter1 = FilterBuilder.For<PersonStud>()
                                                  .And(e => e.Name.Contains("Foo"));

        var extendedFilter = FilterBuilder.Extend(filter1)
                                          .Or(e => e.Name.Contains("test"));                                  
                             
        var result = testPeople.Where(extendedFilter);

        result.Should().NotBeEmpty();
        result.Count().Should().Be(1);
    }

    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnAndFilterThatEvaluatesToTrue_AndTheExtendFilterReturnsNothing_OneResultsIsReturned()
    {
        //arrange
        var testPeople = new List<PersonStud> { new PersonStud { Name = "foo" } };
        Filter<PersonStud> filter1 = FilterBuilder.For<PersonStud>()
                                                  .And(e => e.Name.Contains("foo"));

        var extendedFilter = FilterBuilder.Extend(filter1)
                                          .Or(e => e.Name.Contains("test"));

        var result = testPeople.Where(extendedFilter);

        result.Should().NotBeEmpty();
        result.Count().Should().Be(1);
    }

    [Fact]
    public void WhenAFilterBuilderIsCreated_WithAnExpressionFilter_TheResultCanBeExtended()
    {
        //arrange
        var testPeople = new List<PersonStud> { new PersonStud { Name = "testje" } };
        Expression<Func<PersonStud, bool>> expressionFilter = FilterBuilder.For<PersonStud>()
                                                                           .And(e => e.Name.Contains("Foo"));

        var extendedFilter = FilterBuilder.Extend(expressionFilter)
                                          .Or(e => e.Name.Contains("test"));

        var result = testPeople.Where(extendedFilter);

        result.Should().NotBeEmpty();
        result.Count().Should().Be(1);
    }
}
