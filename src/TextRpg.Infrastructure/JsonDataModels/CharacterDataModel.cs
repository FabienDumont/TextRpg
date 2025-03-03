namespace TextRpg.Infrastructure.JsonDataModels;

public class CharacterDataModel
{
  #region Properties

  public Guid Id { get; init; }
  public string Name { get; init; } = string.Empty;
  public List<Guid> TraitsId { get; init; } = [];

  #endregion
}
