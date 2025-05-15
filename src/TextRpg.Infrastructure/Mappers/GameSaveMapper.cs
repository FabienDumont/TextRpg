﻿using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Mappers;

/// <summary>
///   Mapper for converting between <see cref="GameSave" /> domain models and <see cref="GameSaveDataModel" /> JSON data
///   models.
/// </summary>
public static class GameSaveMapper
{
  #region Methods

  /// <summary>
  ///   Maps a JSON data model to its domain counterpart.
  /// </summary>
  public static GameSave ToDomain(this GameSaveDataModel dataModel)
  {
    return dataModel.Map(i => GameSave.Load(
        i.Id, i.Name, i.PlayerCharacterId, i.World.ToDomain(), i.TextLines.ToDomainCollection()
      )
    );
  }

  /// <summary>
  ///   Maps a collection of JSON data models to domain models.
  /// </summary>
  public static List<GameSave> ToDomainCollection(this IEnumerable<GameSaveDataModel> dataModels)
  {
    return dataModels.MapCollection(ToDomain);
  }

  /// <summary>
  ///   Maps a domain model to its JSON data model counterpart.
  /// </summary>
  public static GameSaveDataModel ToDataModel(this GameSave domain)
  {
    return domain.Map(u => new GameSaveDataModel
      {
        Id = u.Id,
        Name = u.Name,
        PlayerCharacterId = u.PlayerCharacterId,
        World = u.World.ToDataModel(),
        TextLines = u.TextLines.ToDataModelCollection()
      }
    );
  }

  #endregion
}
