namespace TextRpg.Domain;

public class World
{
  #region Properties

  public Guid Id { get; }
  public DateTime CurrentDate { get; private set; }
  public List<Character> Characters { get; }

  #endregion

  #region Ctors

  private World(Guid id, DateTime currentDate, List<Character> characters)
  {
    Id = id;
    CurrentDate = currentDate;
    Characters = characters ?? throw new ArgumentNullException(nameof(characters));
  }

  #endregion

  #region Methods

  public static World Load(Guid id, DateTime currentDate, List<Character> characters)
  {
    return new World(id, currentDate, characters);
  }

  public static World Create(DateTime currentDate, List<Character> characters)
  {
    return new World(Guid.NewGuid(), currentDate, characters);
  }

  public void AdvanceTime(TimeSpan time)
  {
    CurrentDate = CurrentDate.Add(time);
  }

  public void AddCharacter(Character character)
  {
    Characters.Add(character);
  }

  #endregion
}
