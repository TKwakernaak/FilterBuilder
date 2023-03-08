# FilterBuilder
An library that can be used to create dynamic filters / queries using a fluent builder. 

and / or filters:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>(e => true)
                              .And(e => e.Name.Contains("foo"))
                              .Or(e => e.Name.Contains("moot"));

optional filters:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>(e => true)
                              .When(() => sample.IsActive)
                                .Then(e => e.IsActive == true);
              


combine filters:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<PersonStud>(true)
                              .And(e => e.Name.Contains("moot"));
 
    var extendedFilter = FilterBuilder.Extend<PersonStud>(filter)
                                      .Or(e => e.Name.Contains("foo"));
                                    

                              
              
