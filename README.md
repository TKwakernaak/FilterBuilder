# FilterBuilder
An library that can be used to create dynamic filters using a fluent builder. 

and / or filter usage:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>(e => true)
                              .And(e => e.Name.Contains("foo"))
                              .Or(e => e.Name.Contains("moot"));

optional filters usage:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>(e => true)
                              .When(() => sample.IsActive)
                                .Then(e => e.IsActive == true);
              


