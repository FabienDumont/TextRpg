using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Mappers;

public static class GameSaveMapper
{
  #region Methods

  public static GameSave ToDomain(this GameSaveDataModel dataModel)
  {
    return dataModel.Map(i => GameSave.Load(i.Id, i.Name, i.PlayerCharacterId, i.World.ToDomain()));
  }

  public static List<GameSave> ToDomainCollection(this IEnumerable<GameSaveDataModel> dataModels)
  {
    return dataModels.MapCollection(ToDomain);
  }

  public static GameSaveDataModel ToDataModel(this GameSave domain)
  {
    return domain.Map(
      u => new GameSaveDataModel
      {
        Id = u.Id,
        Name = u.Name,
        PlayerCharacterId = u.PlayerCharacterId,
        World = u.World.ToDataModel()
      }
    );
  }

  #endregion
}
