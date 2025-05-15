using MockQueryable.FakeItEasy;
using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class GreetingRepositoryTests
{
  #region Fields

  private readonly List<GreetingDataModel> _greetingData = [];
  private readonly List<ConditionDataModel> _conditionData = [];
  private readonly GreetingRepository _repository;

  #endregion

  #region Ctors

  public GreetingRepositoryTests()
  {
    var context = A.Fake<ApplicationContext>();

    var greetingDbSet = _greetingData.AsQueryable().BuildMockDbSet();
    A.CallTo(() => context.Greetings).Returns(greetingDbSet);

    var conditionDbSet = _conditionData.AsQueryable().BuildMockDbSet();
    A.CallTo(() => context.Conditions).Returns(conditionDbSet);

    A.CallTo(() => context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new GreetingRepository(context);
  }

  #endregion

  #region Methods

  [Fact]
  public async Task GetByRelationshipLevelAsync_ShouldReturnGreeting_WhenMatchExists()
  {
    // Arrange
    var greeting = new GreetingDataModel
    {
      Id = Guid.NewGuid(),
      SpokenText = "Yo!"
    };

    _greetingData.Clear();
    _greetingData.Add(greeting);

    _conditionData.Clear();

    var traits = Enumerable.Empty<Trait>();
    const int relationshipLevel = 15;

    // Act
    var result = await _repository.GetByRelationshipLevelAsync(relationshipLevel, traits, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(greeting.Id, result.Id);
    Assert.Equal(greeting.SpokenText, result.SpokenText);
  }

  [Fact]
  public async Task GetByRelationshipLevelAsync_ShouldReturnNull_WhenConditionsNotMet()
  {
    // Arrange
    var greetingId = Guid.NewGuid();

    var greeting = new GreetingDataModel
    {
      Id = greetingId,
      SpokenText = "Leave me alone!"
    };

    _greetingData.Clear();
    _greetingData.Add(greeting);

    _conditionData.Clear();
    _conditionData.Add(
      new ConditionDataModel
      {
        Id = Guid.NewGuid(),
        ContextType = "Greeting",
        ContextId = greetingId,
        ConditionType = "Relationship",
        Operator = ">",
        OperandRight = "50",
        Negate = false
      }
    );

    var traits = Enumerable.Empty<Trait>();
    const int relationshipLevel = 10;

    // Act
    var result = await _repository.GetByRelationshipLevelAsync(relationshipLevel, traits, CancellationToken.None);

    // Assert
    Assert.Null(result);
  }

  #endregion
}
