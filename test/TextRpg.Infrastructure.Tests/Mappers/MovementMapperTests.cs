using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class MovementMapperTests
{
  #region Methods

  [Fact]
  public void ToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var fromLocationId = Guid.NewGuid();
    var fromRoomId = Guid.NewGuid();
    var toLocationId = Guid.NewGuid();
    var toRoomId = Guid.NewGuid();
    var requiredItemId = Guid.NewGuid();

    var dataModel = new MovementDataModel
    {
      Id = id,
      FromLocationId = fromLocationId,
      FromRoomId = fromRoomId,
      ToLocationId = toLocationId,
      ToRoomId = toRoomId,
      RequiredItemId = requiredItemId
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.FromLocationId.Should().Be(fromLocationId);
    domain.FromRoomId.Should().Be(fromRoomId);
    domain.ToLocationId.Should().Be(toLocationId);
    domain.ToRoomId.Should().Be(toRoomId);
    domain.RequiredItemId.Should().Be(requiredItemId);
  }

  [Fact]
  public void ToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var dataModels = new List<MovementDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        FromLocationId = Guid.NewGuid(),
        FromRoomId = Guid.NewGuid(),
        ToLocationId = Guid.NewGuid(),
        ToRoomId = Guid.NewGuid(),
        RequiredItemId = null
      },
      new()
      {
        Id = Guid.NewGuid(),
        FromLocationId = Guid.NewGuid(),
        FromRoomId = null,
        ToLocationId = Guid.NewGuid(),
        ToRoomId = null,
        RequiredItemId = Guid.NewGuid()
      }
    };

    // Act
    var domainModels = dataModels.ToDomainCollection();

    // Assert
    domainModels.Should().HaveCount(2);
    for (var i = 0; i < domainModels.Count; i++)
    {
      domainModels[i].Id.Should().Be(dataModels[i].Id);
      domainModels[i].FromLocationId.Should().Be(dataModels[i].FromLocationId);
      domainModels[i].FromRoomId.Should().Be(dataModels[i].FromRoomId);
      domainModels[i].ToLocationId.Should().Be(dataModels[i].ToLocationId);
      domainModels[i].ToRoomId.Should().Be(dataModels[i].ToRoomId);
      domainModels[i].RequiredItemId.Should().Be(dataModels[i].RequiredItemId);
    }
  }

  [Fact]
  public void ToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var fromLocationId = Guid.NewGuid();
    var fromRoomId = Guid.NewGuid();
    var toLocationId = Guid.NewGuid();
    var toRoomId = Guid.NewGuid();
    var requiredItemId = Guid.NewGuid();

    var domain = Movement.Load(id, fromLocationId, fromRoomId, toLocationId, toRoomId, requiredItemId);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.FromLocationId.Should().Be(fromLocationId);
    dataModel.FromRoomId.Should().Be(fromRoomId);
    dataModel.ToLocationId.Should().Be(toLocationId);
    dataModel.ToRoomId.Should().Be(toRoomId);
    dataModel.RequiredItemId.Should().Be(requiredItemId);
  }

  #endregion
}
