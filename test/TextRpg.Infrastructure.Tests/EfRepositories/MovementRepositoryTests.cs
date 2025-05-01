using MockQueryable.FakeItEasy;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.EfRepositories;

namespace TextRpg.Infrastructure.Tests.EfRepositories;

public class MovementRepositoryTests
{
  #region Fields

  private readonly Guid _bedroomId = Guid.NewGuid();
  private readonly Guid _homeId = Guid.NewGuid();
  private readonly Guid _livingRoomId = Guid.NewGuid();

  private readonly MovementRepository _repository;
  private readonly Guid _streetId = Guid.NewGuid();

  #endregion

  #region Ctors

  #region Ctor

  public MovementRepositoryTests()
  {
    List<MovementDataModel> movementDataModels =
    [
      // Living Room -> Bedroom (same location)
      new()
      {
        Id = Guid.NewGuid(),
        FromLocationId = _homeId,
        FromRoomId = _livingRoomId,
        ToLocationId = _homeId,
        ToRoomId = _bedroomId,
        RequiredItemId = null
      },
      // Living Room -> Street (different location)
      new()
      {
        Id = Guid.NewGuid(),
        FromLocationId = _homeId,
        FromRoomId = _livingRoomId,
        ToLocationId = _streetId,
        ToRoomId = null,
        RequiredItemId = null
      }
    ];

    var context = A.Fake<ApplicationContext>();

    var movementsDbSet = movementDataModels.AsQueryable().BuildMockDbSet();

    A.CallTo(() => context.Movements).Returns(movementsDbSet);
    A.CallTo(() => context.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(1));

    _repository = new MovementRepository(context);
  }

  #endregion

  #endregion

  #region Tests

  [Fact]
  public async Task GetAvailableMovementsAsync_ShouldReturnMovements_WhenMovementsExist()
  {
    // Act
    var result = await _repository.GetAvailableMovementsAsync(_homeId, _livingRoomId, CancellationToken.None);

    // Assert
    result.Should().NotBeNull();
    result.Should().HaveCount(2);
  }

  [Fact]
  public async Task GetAvailableMovementsAsync_ShouldReturnEmptyList_WhenNoMovementsExist()
  {
    // Act
    var result = await _repository.GetAvailableMovementsAsync(Guid.NewGuid(), null, CancellationToken.None);

    // Assert
    result.Should().NotBeNull();
    result.Should().BeEmpty();
  }

  #endregion
}
