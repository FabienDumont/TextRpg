﻿using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Mappers;

/// <summary>
///   Mapper for converting between <see cref="Location" /> domain models and <see cref="LocationDataModel" /> EF data
///   models.
/// </summary>
public static class LocationMapper
{
  #region Methods

  /// <summary>
  ///   Maps an EF data model to its domain counterpart.
  /// </summary>
  public static Location ToDomain(this LocationDataModel dataModel)
  {
    return dataModel.Map(i => Location.Load(i.Id, i.Name, i.IsAlwaysOpen));
  }

  /// <summary>
  ///   Maps a collection of EF data models to domain models.
  /// </summary>
  public static List<Location> ToDomainCollection(this IEnumerable<LocationDataModel> dataModels)
  {
    return dataModels.MapCollection(ToDomain);
  }

  /// <summary>
  ///   Maps a domain model to its EF data model counterpart.
  /// </summary>
  public static LocationDataModel ToDataModel(this Location domain)
  {
    return domain.Map(u => new LocationDataModel
      {
        Id = u.Id,
        Name = u.Name,
        IsAlwaysOpen = u.IsAlwaysOpen
      }
    );
  }

  #endregion
}
