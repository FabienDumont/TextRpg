namespace TextRpg.Infrastructure.JsonDataModels;

public class GameSaveDataModel
{
  #region Properties

  public Guid Id { get; init; }
  public string Name { get; init; } = string.Empty;
  public Guid PlayerCharacterId { get; init; }
  public List<CharacterDataModel> Characters { get; init; } = [];
  public DateTime SavedAt { get; init; }

  #endregion
}
