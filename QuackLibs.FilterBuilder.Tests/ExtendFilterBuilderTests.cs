using FluentAssertions;

namespace QuackLibs.FilterBuilder.Tests
{
    public class ExtendFilterBuilderTests
    {
        [Fact]
        public void WhenAFilterBuilderIsCreated_WithAnAndFilterThatEvaluatesToTrue_OneResultsIsReturned()
        {
            //arrange
            var testPeople = new List<PersonStud> { new PersonStud { Name = "testje" } };
            var filter1 = FilterBuilder.For<PersonStud>(true)
                                       .And(e => e.Name.Contains("Foo"));

            var extendedFilter = FilterBuilder.Extend<PersonStud>(filter1)
                                              .Or(e => e.Name.Contains("test"));                                       
                                 


            var result = testPeople.Where(extendedFilter);

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
        }
    }
}
