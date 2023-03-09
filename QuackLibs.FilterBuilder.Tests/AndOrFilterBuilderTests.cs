using FluentAssertions;

namespace QuackLibs.FilterBuilder.Tests
{
    public class AndOrFilterBuilderTests
    {
        [Fact]
        public void WhenAFilterBuilderIsCreated_WithAnAndFilterThatEvaluatesToFalse_NoResultsAreReturned()
        {
            //arrange
            var testPeople = new List<PersonStud> { new PersonStud { Name = "testj" } };
            var filter = FilterBuilder.For<PersonStud>()
                                      .And(e => e.Name.Contains("test"))
                                      .And(e => e.Name.Contains("je"));

            Func<PersonStud, bool> compiledFilter = filter;

            var result = testPeople.Where(compiledFilter);


            result.Should().BeEmpty();
            result.Count().Should().Be(0);
        }

        [Fact]
        public void WhenAFilterBuilderIsCreated_WithAnAndFilterThatEvaluatesToTrue_OneResultsIsReturned()
        {
            //arrange
            var testPeople = new List<PersonStud> { new PersonStud { Name = "testje" } };
            var filter = FilterBuilder.For<PersonStud>()
                                      .And(e => e.Name.Contains("test"))
                                      .And(e => e.Name.Contains("je"));

            var result = testPeople.Where(filter);

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void WhenAFilterBuilderIsCreated_WithAnAndAndOrFilterThatEvaluatesToTrue_OneResultIsReturned()
        {
            //arrange
            var testPeople = new List<PersonStud> { new PersonStud { Name = "testj" } };
            var filter = FilterBuilder.For<PersonStud>()
                                      .And(e => e.Name.Contains("test"))
                                      .Or(e => e.Name.Contains("moot"));

            var result = testPeople.Where(filter);

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
        }
    }
}