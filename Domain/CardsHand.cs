using System.Collections;

namespace Domain;

public sealed class CardsHand
{
    public Card[] Cards { get; }

    public static readonly int BasicLength = 5; 

    public CardsHand(
        IReadOnlyCollection<Card> cards) 
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (cards.Count != BasicLength)
        {
            throw new ArgumentException(
                $"Cards count should be equal to {BasicLength}.");
        }

        Cards = cards.ToArray();
    }

    public void ApplySwapCommand(SwappingCommand swapCommand)
    {
        ArgumentNullException.ThrowIfNull(swapCommand);

        foreach (var item in swapCommand.SwapTricks)
        {
            SwapCard(item);
        }
    }

    private void SwapCard(SwapTrick swapTrick)
    {
        ArgumentNullException.ThrowIfNull(swapTrick.NewCard);

        if (swapTrick.CardIndex >= Cards.Length)
        {
            throw new ArgumentException(
                $"Card index of swapping card should be less than {Cards.Length}");
        }

        Cards[swapTrick.CardIndex] = swapTrick.NewCard;
    }
}
