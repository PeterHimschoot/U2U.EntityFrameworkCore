using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace U2U.EntityFrameworkCore.Tests
{
  public class SimpleObject
  {
    public string Property { get; set; }

    public int Nr { get; set; }
  }

  public class ToSpecificationFactoryShould
  {
    [Fact]
    public void UseEqualMemberComparisonToSetProperty()
    {
      var expected = "Hello";
      var spec = new Specification<SimpleObject>(obj => obj.Property == expected);

      var sut = new ToSpecificationFactory();
      var actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec);

      actual.Should().NotBeNull();
      spec.Test(actual).Should().BeTrue(because: "Instance created should pass specification");
      actual.Property.Should().Be(expected);
    }

    [Fact]
    public void UseCompoundAndMemberComparisonToSetProperty()
    {
      var expected = "Hello";
      
      var spec = new Specification<SimpleObject>(obj => obj.Property == expected).And(
        new Specification<SimpleObject>(o => o.Nr == 10));

      var sut = new ToSpecificationFactory();
      var actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec as Specification<SimpleObject>);

      actual.Should().NotBeNull();
      spec.Test(actual).Should().BeTrue(because: "Instance created should pass specification");
      actual.Property.Should().Be(expected);
      actual.Nr.Should().Be(10);
    }

    [Fact]
    public void UseCompoundOrMemberComparisonToSetProperty()
    {
      var expected = "Hello";

      var spec = new Specification<SimpleObject>(obj => obj.Property == expected).And(
        new Specification<SimpleObject>(o => o.Nr == 10));

      var sut = new ToSpecificationFactory();
      var actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec as Specification<SimpleObject>);

      actual.Should().NotBeNull();
      spec.Test(actual).Should().BeTrue(because: "Instance created should pass specification");
      actual.Property.Should().Be(expected);
      actual.Nr.Should().Be(10);
    }

    [Fact]
    public void UseNotEqualMemberComparisonToSetProperty()
    {
      var expected = "Hello";

      var spec = new Specification<SimpleObject>(obj => obj.Property != expected).And(
        new Specification<SimpleObject>(o => o.Nr != 10));

      var sut = new ToSpecificationFactory();
      var actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec as Specification<SimpleObject>);

      actual.Should().NotBeNull();
      spec.Test(actual).Should().BeTrue(because: "Instance created should pass specification");
      actual.Property.Should().NotBe(expected);
      actual.Nr.Should().NotBe(10);
    }

  }
}
