using MockQueryable.FakeItEasy;
using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class GreetingRepositoryTests
{
  #region Fields

  private readonly ApplicationContext _context;
  private readonly List<GreetingDataModel> _greetingData = [];
  private readonly GreetingRepository _repository;

  #endregion

  #region Ctors

  public GreetingRepositoryTests()
  {
    _context = A.Fake<ApplicationContext>();

    var greetingDbSet = _greetingData.AsQueryable().BuildMockDbSet();
    A.CallTo(() => _context.Greetings).Returns(greetingDbSet);
    A.CallTo(() => _context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new GreetingRepository(_context);
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
      MinRelationship = 10,
      MaxRelationship = 20,
      HasTrait = null,
      SpokenText = "Yo!"
    };

    _greetingData.Clear();
    _greetingData.Add(greeting);

    var traits = Enumerable.Empty<Trait>();
    var relationshipLevel = 15;

    var dbSet = _greetingData.AsQueryable().BuildMockDbSet();
    A.CallTo(() => _context.Greetings).Returns(dbSet);

    // Act
    var result = await _repository.GetByRelationshipLevelAsync(relationshipLevel, traits, CancellationToken.None);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(greeting.Id, result.Id);
    Assert.Equal(greeting.SpokenText, result.SpokenText);
  }

  [Fact]
  public async Task GetByRelationshipLevelAsync_ShouldReturnNull_WhenNoMatchExists()
  {
    // Arrange
    _greetingData.Clear();
    _greetingData.Add(
      new GreetingDataModel
      {
        Id = Guid.NewGuid(),
        MinRelationship = 50,
        MaxRelationship = 100,
        HasTrait = null,
        SpokenText = "Hey there"
      }
    );

    var dbSet = _greetingData.AsQueryable().BuildMockDbSet();
    A.CallTo(() => _context.Greetings).Returns(dbSet);

    // Act
    var result = await _repository.GetByRelationshipLevelAsync(10, [], CancellationToken.None);

    // Assert
    Assert.Null(result);
  }

  #endregion
}
