using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Mappers;

public static class TraitMapper
{
  public static Trait ToDomain(this TraitDataModel traitDataModel)
  {
    return traitDataModel.Map(
      i => Trait.Load(i.Id, i.Name)
    );
  }

  public static List<Trait> ToDomainCollection(this IEnumerable<TraitDataModel> traitDataModels)
  {
    return traitDataModels.MapCollection(ToDomain);
  }

  public static TraitDataModel ToDataModel(this Trait trait)
  {
    return trait.Map(
      u => new TraitDataModel
      {
        Id = u.Id,
        Name = u.Name
      }
    );
  }
}
