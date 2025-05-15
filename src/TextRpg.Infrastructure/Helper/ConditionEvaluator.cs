using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Helper;

/// <summary>
///   Evaluates game conditions.
/// </summary>
public static class ConditionEvaluator
{
  /// <summary>
  ///   Evaluates a single condition using the provided game state.
  /// </summary>
  /// <param name="condition">The condition to evaluate.</param>
  /// <param name="relationshipLevel">The current relationship value to compare against.</param>
  /// <param name="traits">The list of traits the player or NPC has.</param>
  /// <returns>True if the condition is met; otherwise, false.</returns>
  public static bool EvaluateCondition(ConditionDataModel condition, int relationshipLevel, IEnumerable<Trait> traits)
  {
    return condition.ConditionType switch
    {
      "Relationship" => CompareInt(
        relationshipLevel, condition.Operator, int.Parse(condition.OperandRight!), condition.Negate
      ),
      "HasTrait" => EvaluateHasTrait(condition, traits),
      _ => true
    };
  }

  /// <summary>
  ///   Evaluates a "HasTrait" condition by checking if the provided traits include the required trait.
  /// </summary>
  /// <param name="condition">The trait-based condition to evaluate.</param>
  /// <param name="traits">The list of traits to search within.</param>
  /// <returns>True if the trait condition is met; otherwise, false.</returns>
  private static bool EvaluateHasTrait(ConditionDataModel condition, IEnumerable<Trait> traits)
  {
    var hasTrait = Guid.TryParse(condition.OperandLeft, out var traitId) && traits.Any(t => t.Id == traitId);

    return condition.Negate ? !hasTrait : hasTrait;
  }

  /// <summary>
  ///   Evaluates a numerical comparison for relationship values using the given operator.
  /// </summary>
  /// <param name="left">The current relationship value.</param>
  /// <param name="op">The comparison operator (e.g., '=', '>=', etc.).</param>
  /// <param name="right">The target value to compare against.</param>
  /// <param name="negate">Whether to negate the result.</param>
  /// <returns>True if the comparison is satisfied; otherwise, false.</returns>
  private static bool CompareInt(int left, string op, int right, bool negate)
  {
    var result = op switch
    {
      "=" => left == right,
      "!=" => left != right,
      ">" => left > right,
      "<" => left < right,
      ">=" => left >= right,
      "<=" => left <= right,
      _ => false
    };

    return negate ? !result : result;
  }
}
