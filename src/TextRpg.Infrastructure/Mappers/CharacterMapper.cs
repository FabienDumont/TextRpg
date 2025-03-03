using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Mappers;

/// <summary>
///   Mapper for converting between <see cref="Character" /> domain models and <see cref="CharacterDataModel" /> JSON data
///   models.
/// </summary>
public static class CharacterMapper
{
  #region Methods

  /// <summary>
  ///   Maps a JSON data model to its domain counterpart.
  /// </summary>
  public static Character ToDomain(this CharacterDataModel dataModel)
  {
    var character = Character.Load(dataModel.Id, dataModel.Name, dataModel.Age, dataModel.BiologicalSex);
    character.AddTraits(dataModel.TraitsId);

    character.MoveTo(dataModel.LocationId, dataModel.RoomId);

    character.Energy = dataModel.Energy;
    character.Money = dataModel.Money;

    return character;
  }

  /// <summary>
  ///   Maps a collection of JSON data models to a collection of domain models.
  /// </summary>
  public static List<Character> ToDomainCollection(this IEnumerable<CharacterDataModel> dataModels)
  {
    return dataModels.MapCollection(ToDomain);
  }

  /// <summary>
  ///   Maps a domain model to its JSON data model counterpart.
  /// </summary>
  public static CharacterDataModel ToDataModel(this Character domain)
  {
    return domain.Map(u => new CharacterDataModel
      {
        Id = u.Id,
        Name = u.Name,
        Age = u.Age,
        BiologicalSex = u.BiologicalSex,
        TraitsId = u.TraitsId,
        LocationId = u.LocationId,
        RoomId = u.RoomId,
        Energy = u.Energy,
        Money = u.Money
      }
    );
  }

  /// <summary>
  ///   Maps a collection of domain models to a collection of JSON data models.
  /// </summary>
  public static List<CharacterDataModel> ToDataModelCollection(this IEnumerable<Character> domains)
  {
    return domains.MapCollection(ToDataModel);
  }

  #endregion
}
