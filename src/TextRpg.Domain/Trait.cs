namespace TextRpg.Domain;

public class Trait
{
  #region Properties

  public Guid Id { get; set; }
  public string Name { get; set; }

  #endregion

  #region Ctors

  private Trait(Guid id, string name)
  {
    Id = id;
    Name = name;
  }

  #endregion

  #region Methods

  public static Trait Create(string name)
  {
    return new Trait(Guid.NewGuid(), name);
  }

  public static Trait Load(Guid id, string name)
  {
    return new Trait(id, name);
  }

  #endregion
}
