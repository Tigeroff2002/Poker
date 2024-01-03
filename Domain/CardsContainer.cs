using Domain.Enums;

namespace Domain;

public sealed class CardsContainer
{
    public int Length { get; private set; }

    public int CurrentIndex { get; private set; }

    public IList<Card> Cards { get; private set; }

    public bool isMixed = false;

    public CardsContainer(GameType gameType)
    {
        var basicCards = GetBasicCards();

        if (gameType == GameType.SmallColoda)
        {
            Length = SMALL_COLODA_LENGTH;
            Cards = basicCards.ToList();
        }
        else
        {
            if (gameType == GameType.PokerColoda)
            {
                basicCards
                    .ToList()
                    .AddRange(GetPokerColodsCards());

                Length = POKER_COLODA_LENGTH;
            }
            else
            {
                basicCards
                    .ToList()
                    .AddRange(
                        GetPokerColodsCards(withJokers: true));

                Length = POKER_COLODA_LENGTH_WITH_JOKERS;
            }

            basicCards
                .OrderBy(x => x.CardType)
                .ThenBy(x => x.SuitType);

            Cards = basicCards.ToList();
        }

        CurrentIndex = 0;
    }

    public bool IsContainerEmpty()
    {
        return Length == 0;
    }

    public void MixCards()
    {
        if (isMixed)
        {
            throw new ArgumentException(
                "Cards container is already mixed.");
        }

        Cards.MixList();

        isMixed = true;
    }

    public IReadOnlyCollection<Card> ApplyTakeCommand(TakeCardsCommand takeCommand)
    {
        ArgumentNullException.ThrowIfNull(takeCommand);

        var count = takeCommand.CardsCount;

        if (count <= 0 || count > MAX_TAKING_CARDS_COUNT)
        {
            throw new ArgumentException(
                $"Taking cards count should be" +
                $" in range from 1 to {MAX_TAKING_CARDS_COUNT}.");
        }

        if (count > Length)
        {
            throw new ArgumentException(
                $"Current cards container length should be" +
                $" at least equal to taking command count {count}.");
        }

        var takingCards = new List<Card>();

        for (var i = 0; i < count; i++)
        {
            takingCards.Add(TakeCard());
        }

        return takingCards;
    }

    private Card TakeCard()
    {
        if (Length == 0)
        {
            throw new ArgumentException("Cards container is empty");
        }

        var card = Cards[CurrentIndex];

        Cards.RemoveAt(CurrentIndex++);

        Length--;

        return card;
    }

    private static IReadOnlyCollection<Card> GetBasicCards()
    {
        var cards = new List<Card>();

        for (var value = CardType.Six; value <= CardType.Ace; value++)
        {
            for (var suit = SuitType.Hearts; suit <= SuitType.Spades; suit++)
            {
                cards.Add(new Card(value, suit));
            }
        }

        return cards;
    }

    private static IReadOnlyCollection<Card> GetPokerColodsCards(bool withJokers = false)
    {
        var cards = new List<Card>();

        for (var value = CardType.Two; value <= CardType.Five; value++)
        {
            for (var suit = SuitType.Hearts; suit <= SuitType.Spades; suit++)
            {
                cards.Add(new Card(value, suit));
            }
        }

        if (withJokers)
        {
            cards.Add(new Joker(SuitType.Hearts));
            cards.Add(new Joker(SuitType.Spades));
        }

        return cards;
    }

    private const int SMALL_COLODA_LENGTH = 36;
    private const int POKER_COLODA_LENGTH = 52;
    private const int POKER_COLODA_LENGTH_WITH_JOKERS = 54;

    private const int MAX_TAKING_CARDS_COUNT = 5;
}
