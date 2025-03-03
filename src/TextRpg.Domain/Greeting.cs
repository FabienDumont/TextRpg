namespace TextRpg.Domain;

public class Greeting
{
  #region Properties

  public Guid Id { get; }
  public int? MinRelationship { get; }
  public int? MaxRelationship { get; }
  public Guid? HasTrait { get; }
  public string SpokenText { get; }

  #endregion

  #region Ctors

  private Greeting(Guid id, int? minRelationship, int? maxRelationship, Guid? hasTrait, string spokenText)
  {
    Id = id;
    MinRelationship = minRelationship;
    MaxRelationship = maxRelationship;
    HasTrait = hasTrait;
    SpokenText = spokenText;
  }

  #endregion

  #region Methods

  public static Greeting Create(int? minRelationship, int? maxRelationship, Guid? hasTrait, string spokenText)
  {
    return new Greeting(Guid.NewGuid(), minRelationship, maxRelationship, hasTrait, spokenText);
  }

  public static Greeting Load(Guid id, int? minRelationship, int? maxRelationship, Guid? hasTrait, string spokenText)
  {
    return new Greeting(id, minRelationship, maxRelationship, hasTrait, spokenText);
  }

  #endregion
}
