namespace VeganMap.Models;

[Flags]
public enum PlaceTypeEnum
{
    Vegan = 0,
    WFPB = 1,
    VegetarianWithVegan = 2,
    AnyWithVegan = 4,
}
