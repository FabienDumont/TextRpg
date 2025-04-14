namespace TextRpg.Infrastructure.JsonDataModels;

public class WorldDataModel
{
  #region Properties

  public Guid Id { get; init; }
  public DateTime CurrentDate { get; init; }
  public List<CharacterDataModel> Characters { get; init; } = [];

  #endregion
}
