using TextRpg.Domain.Tests.Helpers;
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

    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.AddTraits([traitId]);

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, character);

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

    var character = CharacterHelper.GetBasicPlayerCharacter();

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, character);

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

    var character = CharacterHelper.GetBasicPlayerCharacter();

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, character);

    // Assert
    Assert.True(result);
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

    var character = CharacterHelper.GetBasicPlayerCharacter();

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, character);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void EvaluateCondition_UnknownOperator_ReturnsFalse()
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "Energy",
      Operator = "??", // invalid operator
      OperandRight = "50",
      Negate = false,
      ContextType = "Greeting"
    };

    var character = CharacterHelper.GetBasicPlayerCharacter();

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, character);

    // Assert
    Assert.False(result);
  }

  [Theory]
  [InlineData("=", 50, "50", true)]
  [InlineData("!=", 50, "40", true)]
  [InlineData(">", 50, "40", true)]
  [InlineData("<", 50, "60", true)]
  [InlineData(">=", 50, "50", true)]
  [InlineData("<=", 50, "50", true)]
  [InlineData(">", 50, "60", false)]
  public void EvaluateCondition_Energy_Operators_Work(string op, int energy, string right, bool expected)
  {
    // Arrange
    var condition = new ConditionDataModel
    {
      ConditionType = "Energy",
      Operator = op,
      OperandRight = right,
      Negate = false,
      ContextType = "Greeting"
    };

    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = energy;

    // Act
    var result = ConditionEvaluator.EvaluateCondition(condition, character);

    // Assert
    Assert.Equal(expected, result);
  }

}
