using System.Text.RegularExpressions;

namespace TextRpg.Domain;

/// <summary>
///   Builds dynamic text lines for narrative purposes, supporting token replacement (e.g. [PLAYERNAME]).
/// </summary>
public static partial class TextLineBuilder
{
  #region Fields

  /// <summary>
  ///   Compiled regex to match tokens like [PLAYERNAME] in the template.
  /// </summary>
  private static readonly Regex TokenRegex = MyRegex();

  #endregion

  #region Methods

  /// <summary>
  ///   Builds a <see cref="TextLine"/> from a template string by replacing tokens with character-specific data.
  /// </summary>
  /// <param name="template">The text containing tokens like [PLAYERNAME].</param>
  /// <param name="character">The character to use for token replacement.</param>
  /// <returns>A constructed <see cref="TextLine"/> with formatted parts.</returns>
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

  /// <summary>
  ///   Gets the display color for the character name based on biological sex.
  /// </summary>
  /// <param name="c">The character whose name color is determined.</param>
  /// <returns>A color string used in the UI (e.g. "blue", "pink").</returns>
  private static string GetPlayerColor(Character c)
  {
    return c.BiologicalSex switch
    {
      BiologicalSex.Male => "blue",
      BiologicalSex.Female => "pink",
      _ => "purple"
    };
  }

  /// <summary>
  ///   Regex factory method to match token patterns in the form [TOKEN].
  /// </summary>
  [GeneratedRegex(@"\[([A-Z]+)\]", RegexOptions.Compiled)]
  private static partial Regex MyRegex();

  #endregion
}
