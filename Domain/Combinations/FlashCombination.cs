using Domain.Enums;

namespace Domain.Combinations;

public class FlashCombination : Combination
{
    public SuitType SuitType { get; }

    public FlashCombination(
        SuitType suitType,
        CombinationType combinationType)
        : base(combinationType)
    {
        SuitType = suitType;
    }
}
