using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentRecordsApi.Models;
using Xunit;

namespace StudentRecords.Tests;

public class StudentTests
{
    [Fact]
    public void ValidStudent_ShouldPassValidation()
    {
        var student = new Student
        {
            RollNumber = 101,
            Name = "Ranjith",
            Course = "Computer Science",
            Mark1 = 85,
            Mark2 = 90,
            Mark3 = 88
        };

        var results = new List<ValidationResult>();
        var context = new ValidationContext(student);

        bool isValid = Validator.TryValidateObject(
            student, context, results, validateAllProperties: true);

        Assert.True(isValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void InvalidMark_ShouldFailValidation(int mark)
    {
        var student = new Student
        {
            RollNumber = 101,
            Name = "Ranjith",
            Course = "Computer Science",
            Mark1 = mark,
            Mark2 = 90,
            Mark3 = 88
        };

        var results = new List<ValidationResult>();
        var context = new ValidationContext(student);

        bool isValid = Validator.TryValidateObject(
            student, context, results, validateAllProperties: true);

        Assert.False(isValid);
    }
}