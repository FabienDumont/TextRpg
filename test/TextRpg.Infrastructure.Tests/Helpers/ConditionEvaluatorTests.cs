using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Helper;

namespace TextRpg.Infrastructure.Tests.Helpers;

public class ConditionEvaluatorTests
{
  [Fact]
  public void EvaluateCondition_HasTrait_Matches_ReturnsTrue()
  {
    // Arrange
    var traitId = Guid.NewGuid();
    var condition = new ConditionDataModel
    {
      ConditionType = "HasTrait",
      OperandLeft = traitId.ToString(),
      Operator = "=",
      OperandRight = "true",
      Negate = false,
      ContextType = "Greeting"
    };

    var traits = new List<Trait> {Trait.Load(traitId, string.Empty)};

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, relationshipLevel: 0, traits);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void EvaluateCondition_HasTrait_NotPresent_ReturnsFalse()
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "HasTrait",
      OperandLeft = Guid.NewGuid().ToString(),
      Operator = "=",
      OperandRight = "true",
      Negate = false,
      ContextType = "Greeting"
    };

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, relationshipLevel: 0, new List<Trait>());

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void EvaluateCondition_HasTrait_Negated_ReturnsTrueWhenTraitNotPresent()
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "HasTrait",
      OperandLeft = Guid.NewGuid().ToString(),
      Operator = "=",
      OperandRight = "true",
      Negate = true,
      ContextType = "Greeting"
    };

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, relationshipLevel: 0, new List<Trait>());

    // Assert
    Assert.True(result);
  }

  [Theory]
  [InlineData("=", 50, "50", true)]
  [InlineData("!=", 50, "40", true)]
  [InlineData(">", 50, "40", true)]
  [InlineData("<", 50, "60", true)]
  [InlineData(">=", 50, "50", true)]
  [InlineData("<=", 50, "50", true)]
  [InlineData(">", 50, "60", false)]
  public void EvaluateCondition_Relationship_Operators_Work(string op, int level, string right, bool expected)
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "Relationship",
      Operator = op,
      OperandRight = right,
      Negate = false,
      ContextType = "Greeting"
    };

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, relationshipLevel: level, new List<Trait>());

    // Assert
    Assert.Equal(expected, result);
  }

  [Fact]
  public void EvaluateCondition_UnknownConditionType_ReturnsTrue()
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "UnknownType",
      Operator = "=",
      OperandRight = "doesn't matter",
      ContextType = "Greeting"
    };

    const int relationshipLevel = 0;

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, relationshipLevel, new List<Trait>());

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void EvaluateCondition_Relationship_UnknownOperator_ReturnsFalse()
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "Relationship",
      Operator = "??", // invalid operator
      OperandRight = "50",
      Negate = false,
      ContextType = "Greeting"
    };

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, relationshipLevel: 50, traits: new List<Trait>());

    // Assert
    Assert.False(result);
  }

}
