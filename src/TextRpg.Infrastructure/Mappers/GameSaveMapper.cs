using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Mappers;

public static class GameSaveMapper
{
  #region Methods

  public static GameSave ToDomain(this GameSaveDataModel gameSaveDataModel)
  {
    return gameSaveDataModel.Map(
      i => GameSave.Load(i.Id, i.Name, i.PlayerCharacterId, i.Characters.ToDomainCollection())
    );
  }

  public static List<GameSave> ToDomainCollection(this IEnumerable<GameSaveDataModel> gameSaveDataModels)
  {
    return gameSaveDataModels.MapCollection(ToDomain);
  }

  public static GameSaveDataModel ToDataModel(this GameSave gameSave)
  {
    return gameSave.Map(
      u => new GameSaveDataModel
      {
        Id = u.Id,
        Name = u.Name,
        PlayerCharacterId = u.PlayerCharacterId,
        Characters = u.Characters.ToDataModelCollection()
      }
    );
  }

  #endregion
}
