using System.Text.RegularExpressions;

namespace TextRpg.Domain;

public static partial class TextLineBuilder
{
  #region Fields

  private static readonly Regex TokenRegex = MyRegex();

  #endregion

  #region Methods

  public static TextLine BuildNarrationLine(string template, Character character)
  {
    var parts = new List<TextPart>();
    var lastIndex = 0;

    foreach (Match match in TokenRegex.Matches(template))
    {
      if (match.Index > lastIndex)
      {
        parts.Add(new TextPart(null, template.Substring(lastIndex, match.Index - lastIndex)));
      }

      parts.Add(
        match.Value switch
        {
          "[PLAYERNAME]" => new TextPart(GetPlayerColor(character), character.Name),
          _ => new TextPart(null, match.Value)
        }
      );

      lastIndex = match.Index + match.Length;
    }

    if (lastIndex < template.Length)
    {
      parts.Add(new TextPart(null, template[lastIndex..]));
    }

    return new TextLine(parts);
  }

  private static string GetPlayerColor(Character c)
  {
    return c.BiologicalSex switch
    {
      BiologicalSex.Male => "blue",
      BiologicalSex.Female => "pink",
      _ => "purple"
    };
  }

  [GeneratedRegex(@"\[([A-Z]+)\]", RegexOptions.Compiled)]
  private static partial Regex MyRegex();

  #endregion
}
