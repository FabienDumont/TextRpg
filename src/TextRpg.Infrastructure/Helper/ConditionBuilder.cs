using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Helper;

public static class ConditionBuilder
{
  private static ConditionDataModel EnergyCondition(string contextType, Guid contextId, string op, string value) =>
    new()
    {
      Id = Guid.NewGuid(),
      ContextType = contextType,
      ContextId = contextId,
      ConditionType = "Energy",
      Operator = op,
      OperandRight = value,
      Negate = false
    };

  public static IEnumerable<ConditionDataModel> BuildEnergyConditions(
    string contextType, Guid contextId, (string op, string value)[] rules
  ) =>
    rules.Select(rule => EnergyCondition(contextType, contextId, rule.op, rule.value));

  public static ConditionDataModel TraitCondition(
    string contextType, Guid contextId, Guid traitId, bool negate = false
  ) =>
    new()
    {
      Id = Guid.NewGuid(),
      ContextType = contextType,
      ContextId = contextId,
      ConditionType = "HasTrait",
      OperandLeft = traitId.ToString(),
      Operator = "=",
      OperandRight = "true",
      Negate = negate
    };

  public static IEnumerable<ConditionDataModel> BuildEnergyConditions(
    string contextType, Guid contextId, IEnumerable<Guid> traitIds, bool negate = false
  ) =>
    traitIds.Select(id => TraitCondition(contextType, contextId, id, negate));
}
