using Domain.Enums;

namespace Domain;

public record Joker : Card
{
    public SuitColor Color { get; }

    public Joker(SuitType suitType)
        : base(CardType.Joker, suitType)
    {
        Color = suitType.isSuitRed() 
            ? SuitColor.Red 
            : SuitColor.Black;
    }
}
