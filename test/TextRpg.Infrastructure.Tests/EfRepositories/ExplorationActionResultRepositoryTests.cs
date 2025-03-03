using MockQueryable.FakeItEasy;
using TextRpg.Domain.Tests.Helpers;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class ExplorationActionResultRepositoryTests
{
  #region Methods

  [Fact]
  public async Task GetByExplorationActionIdAsync_ShouldReturnMatchingResult()
  {
    // Arrange
    var explorationActionId = Guid.NewGuid();
    var expectedResultId = Guid.NewGuid();

    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 50;
    character.Money = 200;

    var dataModels = new List<ExplorationActionResultDataModel>
    {
      new()
      {
        Id = expectedResultId,
        ExplorationActionId = explorationActionId,
        MinEnergy = 10,
        MaxEnergy = 100,
        MinMoney = 50,
        MaxMoney = 300,
        AddMinutes = true,
        EnergyChange = 10,
        MoneyChange = 20
      },
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionId = explorationActionId,
        MinEnergy = 200,
        MaxEnergy = 300,
        MinMoney = 50,
        MaxMoney = 300,
        AddMinutes = true,
        EnergyChange = 5,
        MoneyChange = 0
      }
    };

    var dbSet = dataModels.AsQueryable().BuildMockDbSet();

    var context = A.Fake<ApplicationContext>();
    A.CallTo(() => context.ExplorationActionResults).Returns(dbSet);

    var repository = new ExplorationActionResultRepository(context);

    // Act
    var result = await repository.GetByExplorationActionIdAsync(explorationActionId, character, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedResultId, result.Id);
  }

  [Fact]
  public async Task GetByExplorationActionIdAsync_ShouldThrow_WhenNoResultFound()
  {
    // Arrange
    var explorationActionId = Guid.NewGuid();

    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 5;
    character.Money = 1;

    var dataModels = new List<ExplorationActionResultDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionId = explorationActionId,
        MinEnergy = 10,
        MaxEnergy = 100,
        MinMoney = 50,
        MaxMoney = 300,
        AddMinutes = true,
        EnergyChange = 10,
        MoneyChange = 20
      }
    };

    var dbSet = dataModels.AsQueryable().BuildMockDbSet();

    var context = A.Fake<ApplicationContext>();
    A.CallTo(() => context.ExplorationActionResults).Returns(dbSet);

    var repository = new ExplorationActionResultRepository(context);

    // Act & Assert
    var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
      await repository.GetByExplorationActionIdAsync(explorationActionId, character, CancellationToken.None)
    );

    Assert.Equal(
      $"No appropriate exploration action result found for exploration action {explorationActionId}.", ex.Message
    );
  }

  #endregion
}
