namespace TextRpg.Domain;

public class Character
{
  #region Properties

  public Guid Id { get; }
  public string Name { get; }
  public List<Guid> TraitsId { get; } = [];

  #endregion

  #region Ctors

  private Character(Guid id, string name)
  {
    Id = id;
    Name = name;
  }

  #endregion

  #region Methods

  public static Character Create(string name)
  {
    return new Character(Guid.NewGuid(), name);
  }

  public static Character Load(Guid id, string name)
  {
    return new Character(id, name);
  }

  public void AddTraits(IEnumerable<Guid> traitIds)
  {
    TraitsId.AddRange(traitIds);
  }

  #endregion
}
