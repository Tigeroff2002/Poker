using Domain.Enums;

namespace Domain.Combinations;

public class StreetCombination : Combination
{
    public int Count { get; } = 5;

    public int BeginnerCardType { get; }

    public StreetCombination(
        int beginnerCardType,
        CombinationType combinationType = CombinationType.Street)
        : base(combinationType)
    {
        BeginnerCardType = beginnerCardType;
    }
}
