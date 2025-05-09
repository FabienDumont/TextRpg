using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;
using TextRpg.Infrastructure.Mappers;

namespace TextRpg.Infrastructure.Tests.Mappers;

public class LocationOpeningHoursMapperTests
{
  #region Methods

  [Fact]
  public void ToDomain_ShouldMapDataModelToDomain()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    const DayOfWeek dayOfWeek = DayOfWeek.Monday;
    var opensAt = new TimeSpan(8, 0, 0);
    var closesAt = new TimeSpan(18, 0, 0);

    var dataModel = new LocationOpeningHoursDataModel
    {
      Id = id,
      LocationId = locationId,
      DayOfWeek = dayOfWeek,
      OpensAt = opensAt,
      ClosesAt = closesAt
    };

    // Act
    var domain = dataModel.ToDomain();

    // Assert
    domain.Id.Should().Be(id);
    domain.LocationId.Should().Be(locationId);
    domain.DayOfWeek.Should().Be(dayOfWeek);
    domain.OpensAt.Should().Be(opensAt);
    domain.ClosesAt.Should().Be(closesAt);
  }

  [Fact]
  public void ToDomainCollection_ShouldMapDataModelCollectionToDomainCollection()
  {
    // Arrange
    var models = new List<LocationOpeningHoursDataModel>
    {
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = Guid.NewGuid(),
        DayOfWeek = DayOfWeek.Tuesday,
        OpensAt = new TimeSpan(9, 0, 0),
        ClosesAt = new TimeSpan(17, 0, 0)
      },
      new()
      {
        Id = Guid.NewGuid(),
        LocationId = Guid.NewGuid(),
        DayOfWeek = DayOfWeek.Wednesday,
        OpensAt = new TimeSpan(10, 0, 0),
        ClosesAt = new TimeSpan(16, 0, 0)
      }
    };

    // Act
    var domains = models.ToDomainCollection();

    // Assert
    domains.Should().HaveCount(2);
    for (var i = 0; i < domains.Count; i++)
    {
      domains[i].Id.Should().Be(models[i].Id);
      domains[i].LocationId.Should().Be(models[i].LocationId);
      domains[i].DayOfWeek.Should().Be(models[i].DayOfWeek);
      domains[i].OpensAt.Should().Be(models[i].OpensAt);
      domains[i].ClosesAt.Should().Be(models[i].ClosesAt);
    }
  }

  [Fact]
  public void ToDataModel_ShouldMapDomainToDataModel()
  {
    // Arrange
    var id = Guid.NewGuid();
    var locationId = Guid.NewGuid();
    var day = DayOfWeek.Thursday;
    var opens = new TimeSpan(7, 0, 0);
    var closes = new TimeSpan(15, 0, 0);

    var domain = LocationOpeningHours.Load(id, locationId, day, opens, closes);

    // Act
    var dataModel = domain.ToDataModel();

    // Assert
    dataModel.Id.Should().Be(id);
    dataModel.LocationId.Should().Be(locationId);
    dataModel.DayOfWeek.Should().Be(day);
    dataModel.OpensAt.Should().Be(opens);
    dataModel.ClosesAt.Should().Be(closes);
  }

  [Fact]
  public void ToDataModelCollection_ShouldMapDomainCollectionToDataModelCollection()
  {
    // Arrange
    var domains = new List<LocationOpeningHours>
    {
      LocationOpeningHours.Load(
        Guid.NewGuid(), Guid.NewGuid(), DayOfWeek.Friday, new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0)
      ),
      LocationOpeningHours.Load(
        Guid.NewGuid(), Guid.NewGuid(), DayOfWeek.Saturday, new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0)
      )
    };

    // Act
    var dataModels = domains.ToDataModelCollection();

    // Assert
    dataModels.Should().HaveCount(2);
    for (var i = 0; i < dataModels.Count; i++)
    {
      dataModels[i].Id.Should().Be(domains[i].Id);
      dataModels[i].LocationId.Should().Be(domains[i].LocationId);
      dataModels[i].DayOfWeek.Should().Be(domains[i].DayOfWeek);
      dataModels[i].OpensAt.Should().Be(domains[i].OpensAt);
      dataModels[i].ClosesAt.Should().Be(domains[i].ClosesAt);
    }
  }

  #endregion
}
