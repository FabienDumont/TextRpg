using TextRpg.Domain;

namespace TextRpg.Infrastructure.JsonDataModels;

public class CharacterDataModel
{
  #region Properties

  public Guid Id { get; init; }
  public string Name { get; init; } = string.Empty;
  public int Age { get; init; }
  public BiologicalSex BiologicalSex { get; init; }
  public List<Guid> TraitsId { get; init; } = [];

  #endregion
}
