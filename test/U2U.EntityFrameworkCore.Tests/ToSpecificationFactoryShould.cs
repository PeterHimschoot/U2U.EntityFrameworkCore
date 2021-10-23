namespace U2U.EntityFrameworkCore.Tests;

public class SimpleObject
{
  public string Property { get; set; } = string.Empty;

  public int Nr { get; set; }
}

public class ToSpecificationFactoryShould
{
  [Fact]
  public void UseEqualMemberComparisonToSetProperty()
  {
    string? expected = "Hello";
    var spec = new Specification<SimpleObject>(obj => obj.Property == expected);

    var sut = new ToSpecificationFactory();
    SimpleObject? actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec);

    actual.Should().NotBeNull();
    spec.Test(actual!).Should().BeTrue(because: "Instance created should pass specification");
    actual!.Property.Should().Be(expected);
  }

  [Fact]
  public void UseCompoundAndMemberComparisonToSetProperty()
  {
    string? expected = "Hello";

    ISpecification<SimpleObject> spec = new Specification<SimpleObject>(obj => obj.Property == expected).And(
        new Specification<SimpleObject>(o => o.Nr == 10));

    var sut = new ToSpecificationFactory();
    SimpleObject? actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec);

    actual.Should().NotBeNull();
    spec.Test(actual!).Should().BeTrue(because: "Instance created should pass specification");
    actual!.Property.Should().Be(expected);
    actual.Nr.Should().Be(10);
  }

  [Fact]
  public void UseCompoundOrMemberComparisonToSetProperty()
  {
    string? expected = "Hello";

    Abstractions.ISpecification<SimpleObject>? spec = new Specification<SimpleObject>(obj => obj.Property == expected).And(
        new Specification<SimpleObject>(o => o.Nr == 10));

    var sut = new ToSpecificationFactory();
    SimpleObject? actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec);

    actual.Should().NotBeNull();
    spec.Test(actual!).Should().BeTrue(because: "Instance created should pass specification");
    actual!.Property.Should().Be(expected);
    actual.Nr.Should().Be(10);
  }

  [Fact]
  public void UseNotEqualMemberComparisonToSetProperty()
  {
    string? expected = "Hello";

    Abstractions.ISpecification<SimpleObject>? spec = new Specification<SimpleObject>(obj => obj.Property != expected).And(
        new Specification<SimpleObject>(o => o.Nr != 10));

    var sut = new ToSpecificationFactory();
    SimpleObject? actual = ToSpecificationFactory.CreateFromSpecification<SimpleObject>(spec);

    actual.Should().NotBeNull();
    spec.Test(actual!).Should().BeTrue(because: "Instance created should pass specification");
    actual!.Property.Should().NotBe(expected);
    actual.Nr.Should().NotBe(10);
  }

}

