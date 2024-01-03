using Domain.Enums;

namespace Domain.Combinations;

public sealed class CombinationWithSeniorCard : Combination
{
    public CardType SeniorCardType { get; }

    public CombinationWithSeniorCard(
        CardType seniorCardType,
        CombinationType combinationType)
        : base(combinationType)
    {
        SeniorCardType = seniorCardType;
    }
}
