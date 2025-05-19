using MockQueryable.FakeItEasy;
using TextRpg.Domain.Tests.Helpers;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class ExplorationActionResultRepositoryTests
{
  [Fact]
  public async Task GetByExplorationActionIdAsync_ShouldReturnMatchingResult_WhenConditionsAreMet()
  {
    // Arrange
    var explorationActionId = Guid.NewGuid();
    var matchingResultId = Guid.NewGuid();

    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 50;
    character.Money = 200;

    var results = new List<ExplorationActionResultDataModel>
    {
      new()
      {
        Id = matchingResultId,
        ExplorationActionId = explorationActionId,
        AddMinutes = true,
        EnergyChange = 10,
        MoneyChange = 20
      },
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionId = explorationActionId,
        AddMinutes = true,
        EnergyChange = 5,
        MoneyChange = 0
      }
    };

    var conditions = new List<ConditionDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        ContextType = "ExplorationActionResult",
        ContextId = matchingResultId,
        ConditionType = "Energy",
        Operator = ">=",
        OperandRight = "50",
        Negate = false
      },
      new()
      {
        Id = Guid.NewGuid(),
        ContextType = "ExplorationActionResult",
        ContextId = matchingResultId,
        ConditionType = "Money",
        Operator = ">",
        OperandRight = "100",
        Negate = false
      }
    };

    var resultDbSet = results.AsQueryable().BuildMockDbSet();
    var conditionDbSet = conditions.AsQueryable().BuildMockDbSet();

    var context = A.Fake<ApplicationContext>();
    A.CallTo(() => context.ExplorationActionResults).Returns(resultDbSet);
    A.CallTo(() => context.Conditions).Returns(conditionDbSet);

    var repo = new ExplorationActionResultRepository(context);

    // Act
    var result = await repo.GetByExplorationActionIdAsync(explorationActionId, character, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(matchingResultId, result.Id);
  }

  [Fact]
  public async Task GetByExplorationActionIdAsync_ShouldThrow_WhenNoMatchingResultExists()
  {
    // Arrange
    var explorationActionId = Guid.NewGuid();
    var resultId = Guid.NewGuid();

    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 10;
    character.Money = 5;

    var results = new List<ExplorationActionResultDataModel>
    {
      new()
      {
        Id = resultId,
        ExplorationActionId = explorationActionId,
        AddMinutes = true,
        EnergyChange = 10,
        MoneyChange = 20
      }
    };

    var conditions = new List<ConditionDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        ContextType = "ExplorationActionResult",
        ContextId = resultId,
        ConditionType = "Energy",
        Operator = ">=",
        OperandRight = "50",
        Negate = false
      }
    };

    var resultDbSet = results.AsQueryable().BuildMockDbSet();
    var conditionDbSet = conditions.AsQueryable().BuildMockDbSet();

    var context = A.Fake<ApplicationContext>();
    A.CallTo(() => context.ExplorationActionResults).Returns(resultDbSet);
    A.CallTo(() => context.Conditions).Returns(conditionDbSet);

    var repo = new ExplorationActionResultRepository(context);

    // Act & Assert
    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
      repo.GetByExplorationActionIdAsync(explorationActionId, character, CancellationToken.None)
    );

    Assert.Equal(
      $"No appropriate exploration action result found for exploration action {explorationActionId}.", ex.Message
    );
  }
}
