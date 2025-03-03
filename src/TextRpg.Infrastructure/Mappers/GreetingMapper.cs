using TextRpg.Application.Helpers;
using TextRpg.Domain;
using TextRpg.Infrastructure.EfDataModels;

namespace TextRpg.Infrastructure.Mappers;

public static class GreetingMapper
{
  public static Greeting ToDomain(this GreetingDataModel greetingDataModel)
  {
    return greetingDataModel.Map(
      i => Greeting.Load(i.Id, i.MinRelationship, i.MaxRelationship, i.HasTrait, i.SpokenText)
    );
  }

  public static List<Greeting> ToDomainCollection(this IEnumerable<GreetingDataModel> greetingDataModels)
  {
    return greetingDataModels.MapCollection(ToDomain);
  }

  public static GreetingDataModel ToDataModel(this Greeting greeting)
  {
    return greeting.Map(
      u => new GreetingDataModel
      {
        Id = u.Id,
        MinRelationship = u.MinRelationship,
        MaxRelationship = u.MaxRelationship,
        HasTrait = u.HasTrait,
        SpokenText = u.SpokenText
      }
    );
  }
}
