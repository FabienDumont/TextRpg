using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Tests.EfDataModels;

public class ConditionDataModelTests
{
  #region Methods

  [Fact]
  public void Instantiation_ShouldInitializeWithAllValues()
  {
    // Arrange
    var id = Guid.NewGuid();
    var contextId = Guid.NewGuid();

    const string contextType = "Greeting";
    const string conditionType = "HasTrait";
    const string operandLeft = "trait-id";
    const string @operator = "=";
    const string operandRight = "true";
    const bool negate = true;

    // Act
    var condition = new ConditionDataModel
    {
      Id = id,
      ContextType = contextType,
      ContextId = contextId,
      ConditionType = conditionType,
      OperandLeft = operandLeft,
      Operator = @operator,
      OperandRight = operandRight,
      Negate = negate
    };

    // Assert
    Assert.Equal(id, condition.Id);
    Assert.Equal(contextType, condition.ContextType);
    Assert.Equal(contextId, condition.ContextId);
    Assert.Equal(conditionType, condition.ConditionType);
    Assert.Equal(operandLeft, condition.OperandLeft);
    Assert.Equal(@operator, condition.Operator);
    Assert.Equal(operandRight, condition.OperandRight);
    Assert.True(condition.Negate);
  }

  [Fact]
  public void Instantiation_ShouldAllowNullOperands()
  {
    // Arrange
    var id = Guid.NewGuid();
    var contextId = Guid.NewGuid();

    // Act
    var condition = new ConditionDataModel
    {
      Id = id,
      ContextType = "DialogueOption",
      ContextId = contextId,
      ConditionType = "Relationship",
      Operator = ">=",
      OperandLeft = null,
      OperandRight = null,
      Negate = false
    };

    // Assert
    Assert.Equal("DialogueOption", condition.ContextType);
    Assert.Null(condition.OperandLeft);
    Assert.Null(condition.OperandRight);
    Assert.False(condition.Negate);
  }

  #endregion
}
