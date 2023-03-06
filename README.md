# FilterBuilder
An easy library that can be used to create dynamic filters using expressions.

usage:

    var sample = new List<SampleClass> { new SampleClass { Name = "foo" } };
    var filter = FilterBuilder.For<SampleClass>(e => true)
                              .And(e => e.Name.Contains("foo"))
                              .Or(e => e.Name.Contains("moot"));
             
                                       
              


