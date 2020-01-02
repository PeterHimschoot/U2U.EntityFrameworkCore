#nullable enable

using FluentAssertions;
using U2U.EntityFrameworkCore.Abstractions;
using U2U.EntityFrameworkCore.TestData;
using Xunit;

namespace U2U.EntityFrameworkCore.Tests
{
  public class SpecificationShould
    : IClassFixture<SpecificationFactories>
  {
    private readonly SpecificationFactories specificationFactories;

    public SpecificationShould(SpecificationFactories specificationFactories)
      => this.specificationFactories = specificationFactories;

    [Fact]
    public void ReturnTrueForEqualSpecifications()
    {
      ISpecification<Student> specification1 = this.specificationFactories.For<Student>().Where(student => student.FirstName == "Jefke");
      specification1 = specification1.And(student => student.LastName == "Versmossen");

      ISpecification<Student> specification2 = this.specificationFactories.For<Student>().Where(student => student.FirstName == "Jefke");
      specification2 = specification2.And(student => student.LastName == "Versmossen");

      specification1.Should().Be(specification2);
      specification2.Should().Be(specification1);
    }

    [Fact]
    public void ReturnTrueForSameSpecifications()
    {
      ISpecification<Student> specification1 = this.specificationFactories.For<Student>().Where(student => student.FirstName == "Jefke");
      specification1 = specification1.And(student => student.LastName == "Versmossen");
      specification1.Should().Be(specification1);
    }

    [Fact]
    public void ReturnFalseForNonEqualSpecifications()
    {
      ISpecification<Student> specification1 = this.specificationFactories.For<Student>().Where(student => student.FirstName == "Jefke");
      specification1 = specification1.And(student => student.LastName == "Versmossen");

      ISpecification<Student> specification2 = this.specificationFactories.For<Student>().Where(student => student.FirstName == "Jefke");
      specification2 = specification2.And(student => student.LastName == "Vandermotten");

      specification1.Should().NotBe(specification2);
      specification2.Should().NotBe(specification1);
    }
  }
}
