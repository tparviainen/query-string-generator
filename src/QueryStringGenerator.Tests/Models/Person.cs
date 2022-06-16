﻿namespace QueryStringGenerator.Tests.Models;

[QueryString]
public partial class Person
{
    public string? FirstName { get; set; }
    public string LastName { get; set; } = default!;
    public int? Age { get; set; }
}
