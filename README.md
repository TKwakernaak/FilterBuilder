# FilterBuilder
An library that can be used to create dynamic filters / queries using a fluent builder

The following filters can be used interchangeable
    
    Filter<SampleClass> filter = FilterBuilder.For<SampleClass>()
                                              .And(e => e.Name.Contains("Foo"));
                                             
    Func<SampleClass, bool> funcFilter = FilterBuilder.For<SampleClass>()
                                                      .And(e => e.Name.Contains("Foo"))       
                                                     
    Expression<Func<SampleClass, bool>> expressionFilter = FilterBuilder.For<SampleClass>()
                                                                        .And(e => e.Name.Contains("Foo"))
                                                                              
## Usage:

and / or filters:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>()
                              .And(e => e.Name.Contains("foo"))
                              .Or(e => e.Name.Contains("moot"));

optional filters:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>()
                              .When(() => sample.IsActive)
                                .Then(e => e.IsActive == true);
              


combine filters:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<PersonStud>()
                              .And(e => e.Name.Contains("moot"));
 
    var extendedFilter = FilterBuilder.Extend<PersonStud>(filter)
                                      .Or(e => e.Name.Contains("foo"));
                                    

                              
              
