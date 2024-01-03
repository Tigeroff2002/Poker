using Domain.Combinations;
using Domain.Enums;
using Microsoft.VisualBasic;

namespace Domain;

public sealed class CombinationsExplorer
{
    private IList<Card> _cards;

    private readonly int _length;

    private CombinationsExplorer(IReadOnlyCollection<Card> cards)
    {
        _cards = cards.ToList()
            ?? throw new ArgumentNullException(nameof(cards));

        _length = cards.Count;
    }

    public static CombinationsExplorer GetInstance(IReadOnlyCollection<Card> cards)
    {
        return new CombinationsExplorer(cards);
    }

    public Combination FindEasyCombinations()
    {
        SortCardsByValue();

        var seniorCard = _cards.Last();

        var seniorCardCombination = 
            new CombinationWithSeniorCard(
                seniorCard.CardType,
                CombinationType.SeniorCard);

        if (TryToFindRepeatableCards(out var seniorCombination))
        {
            seniorCardCombination = seniorCombination;
        }

        return seniorCardCombination;
    }

    public bool TryToFindRepeatableCards(
        out CombinationWithSeniorCard combination)
    {
        if (isDistinctingByCardTypes())
        {
            combination = null!;

            return false;
        }

        combination = FindMaxCardTypeCombination();

        return true;
    }

    private CombinationWithSeniorCard FindMaxCardTypeCombination()
    {
        var seniorCombine = (CardType) byte.MinValue;
        var maxRepeatCount = 1;

        foreach (var card in _cards)
        {
            var cardsWithSameValue =
                _cards.Where(
                    x => x.CardType == card.CardType
                    && !x.Equals(card));

            if (cardsWithSameValue.Any())
            {
                var repeatableCardsCount = cardsWithSameValue.Count();

                if (repeatableCardsCount > maxRepeatCount)
                {
                    maxRepeatCount = repeatableCardsCount;
                    seniorCombine = card.CardType;
                }
                else if (repeatableCardsCount == maxRepeatCount)
                {
                    if (card.CardType > seniorCombine)
                    {
                        seniorCombine = card.CardType;
                    }
                }
            }
        }

        return new(
                seniorCombine,
                (CombinationType)maxRepeatCount);
    }

    private bool isDistinctingByCardTypes()
    {
        return _cards
            .DistinctBy(x => x.CardType)
            .Count() == _length;
    }

    private void SortCardsByValue()
    {
        _cards.OrderBy(x => x.CardType);
    }

    private void SortCardsBySuit()
    {
        _cards.OrderBy(x => x.SuitType);
    }
}
