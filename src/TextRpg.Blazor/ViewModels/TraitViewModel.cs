namespace TextRpg.Blazor.ViewModels;

public class TraitViewModel
{
  public Guid Id { get; init; }
  public string Name { get; init; } = "";
  public bool IsSelected { get; set; }
  public bool IsEnabled { get; set; } = true;
}
