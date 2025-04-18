namespace TextRpg.Domain;

public class Character
{
  #region Properties

  public Guid Id { get; }
  public string Name { get; }
  public int Age { get; }
  public BiologicalSex BiologicalSex { get; }
  public List<Guid> TraitsId { get; } = [];

  #endregion

  #region Ctors

  private Character(Guid id, string name, int age, BiologicalSex biologicalSex)
  {
    Id = id;
    Name = name;
    Age = age;
    BiologicalSex = biologicalSex;
  }

  #endregion

  #region Methods

  public static Character Create(string name, int age, BiologicalSex biologicalSex)
  {
    return new Character(Guid.NewGuid(), name, age, biologicalSex);
  }

  public static Character Load(Guid id, string name, int age, BiologicalSex biologicalSex)
  {
    return new Character(id, name, age, biologicalSex);
  }

  public void AddTraits(IEnumerable<Guid> traitIds)
  {
    TraitsId.AddRange(traitIds);
  }

  #endregion
}

public enum BiologicalSex {
  Male,
  Female
}
