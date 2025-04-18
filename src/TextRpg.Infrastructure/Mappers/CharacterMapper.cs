using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.JsonDataModels;

namespace TextRpg.Infrastructure.Mappers;

public static class CharacterMapper
{
  #region Methods

  public static Character ToDomain(this CharacterDataModel characterDataModel)
  {
    var character = Character.Load(
      characterDataModel.Id, characterDataModel.Name, characterDataModel.Age, characterDataModel.BiologicalSex
    );
    character.AddTraits(characterDataModel.TraitsId);
    return character;
  }

  public static List<Character> ToDomainCollection(this IEnumerable<CharacterDataModel> characterDataModels)
  {
    return characterDataModels.MapCollection(ToDomain);
  }

  public static CharacterDataModel ToDataModel(this Character character)
  {
    return character.Map(u => new CharacterDataModel
      {
        Id = u.Id,
        Name = u.Name,
        Age = u.Age,
        BiologicalSex = u.BiologicalSex,
        TraitsId = u.TraitsId
      }
    );
  }

  public static List<CharacterDataModel> ToDataModelCollection(this IEnumerable<Character> characters)
  {
    return characters.MapCollection(ToDataModel);
  }

  #endregion
}
