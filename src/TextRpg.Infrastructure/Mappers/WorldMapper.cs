using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Mappers;

public static class WorldMapper
{
  #region Methods

  public static World ToDomain(this WorldDataModel? dataModel)
  {
    return dataModel.Map(i => World.Load(i.Id, i.CurrentDate, i.Characters.ToDomainCollection()));
  }

  public static List<World> ToDomainCollection(this IEnumerable<WorldDataModel> dataModels)
  {
    return dataModels.MapCollection(ToDomain);
  }

  public static WorldDataModel ToDataModel(this World domain)
  {
    return domain.Map(
      u => new WorldDataModel
      {
        Id = u.Id,
        CurrentDate = u.CurrentDate,
        Characters = u.Characters.ToDataModelCollection()
      }
    );
  }

  #endregion
}
