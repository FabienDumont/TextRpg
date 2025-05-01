using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class LocationRepositoryTests
{
  #region Fields

  private readonly ApplicationContext _context;
  private readonly List<LocationDataModel> _locationDataModels;

  private readonly LocationRepository _repository;

  #endregion

  #region Ctors

  #region Ctor

  public LocationRepositoryTests()
  {
    _locationDataModels =
    [
      new LocationDataModel {Id = Guid.NewGuid(), Name = "Home", IsPlayerSpawn = true},
      new LocationDataModel {Id = Guid.NewGuid(), Name = "Street", IsPlayerSpawn = false}
    ];

    _context = A.Fake<ApplicationContext>();

    var locationsDbSet = _locationDataModels.AsQueryable().BuildMockDbSet();

    A.CallTo(() => _context.Locations).Returns(locationsDbSet);
    A.CallTo(() => _context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new LocationRepository(_context);
  }

  #endregion

  #endregion

  #region Tests

  [Fact]
  public async Task GetByIdAsync_ShouldReturnLocation_WhenLocationExists()
  {
    // Arrange
    var existingLocation = _locationDataModels.First();

    var fakeContext = A.Fake<ApplicationContext>();

    // Mock Locations DbSet
    var locationsDbSet = _locationDataModels.AsQueryable().BuildMockDbSet();

    A.CallTo(() => fakeContext.Locations).Returns(locationsDbSet);

    // Explicitly mock FindAsync behavior
    A.CallTo(() => fakeContext.Locations.FindAsync(
        A<object[]>.That.Matches(keys => (Guid) keys[0] == existingLocation.Id), A<CancellationToken>._
      )
    ).Returns(new ValueTask<LocationDataModel?>(existingLocation));

    var repository = new LocationRepository(fakeContext);

    // Act
    var result = await repository.GetByIdAsync(existingLocation.Id, CancellationToken.None);

    // Assert
    result.Should().NotBeNull();
    result.Id.Should().Be(existingLocation.Id);
    result.Name.Should().Be(existingLocation.Name);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldThrowInvalidOperationException_WhenLocationDoesNotExist()
  {
    // Arrange
    var randomId = Guid.NewGuid();

    A.CallTo(() => _context.Locations.FindAsync(A<object[]>.Ignored, A<CancellationToken>._))
      .Returns(new ValueTask<LocationDataModel?>((LocationDataModel?) null));

    // Act
    Func<Task> act = async () => await _repository.GetByIdAsync(randomId, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>()
      .WithMessage($"Location with ID {randomId} was not found.");
  }

  [Fact]
  public async Task GetPlayerSpawnAsync_ShouldReturnPlayerSpawn_WhenSpawnExists()
  {
    // Act
    var result = await _repository.GetPlayerSpawnAsync(CancellationToken.None);

    // Assert
    result.Should().NotBeNull();
    result.Name.Should().Be("Home");
  }

  [Fact]
  public async Task GetPlayerSpawnAsync_ShouldThrowInvalidOperationException_WhenNoSpawnExists()
  {
    // Arrange
    var contextWithoutSpawn = A.Fake<ApplicationContext>();

    var noSpawnLocations = new List<LocationDataModel>
    {
      new() {Id = Guid.NewGuid(), Name = "Forest", IsPlayerSpawn = false}
    }.AsQueryable().BuildMockDbSet();

    A.CallTo(() => contextWithoutSpawn.Locations).Returns(noSpawnLocations);

    var repositoryWithoutSpawn = new LocationRepository(contextWithoutSpawn);

    // Act
    Func<Task> act = async () => await repositoryWithoutSpawn.GetPlayerSpawnAsync(CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Spawn location not found.");
  }

  #endregion
}
