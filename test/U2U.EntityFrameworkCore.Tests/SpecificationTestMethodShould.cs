#nullable enable

using FluentAssertions;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.TestData;
using Xunit;

namespace U2U.EntityFrameworkCore.Tests
{
  public class SpecificationTestMethodShould
    : IClassFixture<SpecificationFactories>
  {
    private readonly SpecificationFactories specificationFactories;

    public SpecificationTestMethodShould(SpecificationFactories specificationFactories)
      => this.specificationFactories = specificationFactories;

    [Fact]
    public void AllowCriteriaToBeEvaluated()
    {
      var student = new Student(5) { FirstName = "Jefke", LastName = "Versmossen" };

      ISpecification<Student> specification = this.specificationFactories.For<Student>().WithId(5);
      specification.Test(student).Should().BeTrue();

      specification = this.specificationFactories.For<Student>().Where(student => student.FirstName == "Jefke");
      specification.Test(student).Should().BeTrue();

      specification = specification.And(student => student.LastName == "Versmossen");
      specification.Test(student).Should().BeTrue();

      specification = this.specificationFactories.For<Student>().WithId(6);
      specification.Test(student).Should().BeFalse();
    }
  }
}
