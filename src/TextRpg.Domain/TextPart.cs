namespace TextRpg.Domain;

/// <summary>
/// Class representing a part of text with an optional color.
/// </summary>
public class TextPart
{
  public string? Color { get; set; }
  public string Text { get; set; }

  public TextPart(string? color, string text)
  {
    Color = color;
    Text = text;
  }
}
