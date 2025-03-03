using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class NarrationRepositoryTests
{
  #region Fields

  private const string _expectedKey = "intro_scene";
  private const string _expectedText = "You step into the dark forest.";
  private readonly NarrationRepository _repository;

  #endregion

  #region Ctors

  public NarrationRepositoryTests()
  {
    var narrationDataModels = new List<NarrationDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        Key = _expectedKey,
        Text = _expectedText
      }
    };

    var context = A.Fake<ApplicationContext>();
    var narrationDbSet = narrationDataModels.AsQueryable().BuildMockDbSet();

    A.CallTo(() => context.Narrations).Returns(narrationDbSet);
    A.CallTo(() => context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new NarrationRepository(context);
  }

  #endregion

  #region Tests

  [Fact]
  public async Task GetNarrationByKeyAsync_ShouldReturnNarration_WhenExists()
  {
    // Act
    var result = await _repository.GetNarrationByKeyAsync(_expectedKey, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(_expectedKey, result.Key);
    Assert.Equal(_expectedText, result.Text);
  }

  [Fact]
  public async Task GetNarrationByKeyAsync_ShouldThrow_WhenNarrationDoesNotExist()
  {
    // Arrange
    const string nonExistentKey = "non_existent_scene";

    // Act & Assert
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
      await _repository.GetNarrationByKeyAsync(nonExistentKey, CancellationToken.None)
    );

    Assert.Equal($"No narration found for key {nonExistentKey}", exception.Message);
  }

  #endregion
}
