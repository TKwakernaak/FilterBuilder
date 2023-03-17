# FilterBuilder
A library that can be used to create dynamic filters / queries using a fluent builder

The following filters can be used interchangeable
    
    Filter<Sample> filter = FilterBuilder.For<Sample>()
                                         .And(e => e.Name.Contains("Foo"));
                                             
    Func<Sample, bool> funcFilter = FilterBuilder.For<Sample>()
                                                 .And(e => e.Name.Contains("Foo"))       
                                                     
    Expression<Func<Sample, bool>> expressionFilter = FilterBuilder.For<Sample>()
                                                                   .And(e => e.Name.Contains("Foo"))
                                                                              
## Usage:

and / or filters:

    var sample = new List<Sample> { new Sample { Name = "foo" } };
    var filter = FilterBuilder.For<Sample>()
                              .And(e => e.Name.Contains("foo"))
                              .Or(e => e.Name.Contains("moot"));

optional filters:

    var sample = new List<Sample> { new Sample { Name = "foo" } };
    var filter = FilterBuilder.For<Sample>()
                              .When(() => sample.IsActive)
                                .Then(e => e.IsActive == true);
              


combine filters:

    var sample = new List<Sample> { new Sample { Name = "foo" } };
    var filter = FilterBuilder.For<PersonStud>()
                              .And(e => e.Name.Contains("moot"));
 
    var extendedFilter = FilterBuilder.Extend<PersonStud>(filter)
                                      .Or(e => e.Name.Contains("foo"));
                                    

                              
              
## Execution:
    var sample = new List<Sample> { new Sample { Name = "foo" } };
    var filter = FilterBuilder.For<PersonStud>()
                              .And(e => e.Name.Contains("moot")); 
                              
    //execute expression tree in entity framework. The filter (as is the case with all expressions) should not contain code that cannot be converted to sql.
    return await dbcontext.Sample
                          .Where(filter)
                          .ToListAsync();
                          
    //execute func
    var sample = new List<Sample> { new Sample { Name = "bla" } };
    var filter = FilterBuilder.For<PersonStud>()
                              .And(e => e.Name.Contains("bl")); 
                              
    //returns the item in the list                      
    var result = sample.Where(filter)
                       .ToList();
                              
                     
             
                              
