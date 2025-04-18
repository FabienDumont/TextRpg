namespace TextRpg.Infrastructure.JsonDataModels;

public class GameSaveDataModel
{
  #region Properties

  public Guid Id { get; init; }
  public string Name { get; init; } = string.Empty;
  public required WorldDataModel World { get; init; }
  public Guid PlayerCharacterId { get; init; }
  public DateTime SavedAt { get; init; }

  #endregion
}
