using MockQueryable.FakeItEasy;
using TextRpg.Domain.Tests.Helpers;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class ExplorationActionResultNarrationRepositoryTests
{
  #region Fields

  private readonly Guid _resultId = Guid.NewGuid();

  #endregion

  #region Methods

  [Fact]
  public async Task GetByExplorationActionResultIdAsync_ShouldReturnMatchingNarration_WhenCharacterMeetsConditions()
  {
    // Arrange
    var matchingId = Guid.NewGuid();
    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 45;

    var dataModels = new List<ExplorationActionResultNarrationDataModel>
    {
      new()
      {
        Id = matchingId,
        ExplorationActionResultId = _resultId,
        MinEnergy = 30,
        MaxEnergy = 60,
        Text = "You lie down with a heavy sigh."
      },
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = _resultId,
        MinEnergy = 0,
        MaxEnergy = 20,
        Text = "You crash into bed instantly."
      }
    };

    var dbSet = dataModels.AsQueryable().BuildMockDbSet();
    var context = A.Fake<ApplicationContext>();
    A.CallTo(() => context.ExplorationActionResultNarrations).Returns(dbSet);

    var repo = new ExplorationActionResultNarrationRepository(context);

    // Act
    var result = await repo.GetByExplorationActionResultIdAsync(_resultId, character, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(matchingId, result.Id);
    Assert.Equal("You lie down with a heavy sigh.", result.Text);
  }

  [Fact]
  public async Task GetByExplorationActionResultIdAsync_ShouldThrow_WhenNoNarrationMatches()
  {
    // Arrange
    var character = CharacterHelper.GetBasicPlayerCharacter();
    character.Energy = 80;

    var dataModels = new List<ExplorationActionResultNarrationDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        ExplorationActionResultId = _resultId,
        MinEnergy = 0,
        MaxEnergy = 50,
        Text = "You're completely drained."
      }
    };

    var dbSet = dataModels.AsQueryable().BuildMockDbSet();
    var context = A.Fake<ApplicationContext>();
    A.CallTo(() => context.ExplorationActionResultNarrations).Returns(dbSet);

    var repo = new ExplorationActionResultNarrationRepository(context);

    // Act & Assert
    var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
      repo.GetByExplorationActionResultIdAsync(_resultId, character, CancellationToken.None)
    );

    Assert.Equal(
      $"No appropriate exploration action result narration found for exploration action result {_resultId}.", ex.Message
    );
  }

  #endregion
}
