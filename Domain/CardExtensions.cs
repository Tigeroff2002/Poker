using Domain.Enums;
using System.Security.Cryptography;

namespace Domain;

public static class CardExtensions
{
    public static bool isSuitRed(this SuitType type)
    {
        return type is SuitType.Hearts or SuitType.Diamonds;
    }

    public static void MixList<Card>(this IList<Card> list)
    {
        var count = list.Count;

        while (count > 1)
        {
            var randomIndex = RandomNumberGenerator.GetInt32(--count + 1);
            var value = list[randomIndex];
            list[randomIndex] = list[count];
            list[count] = value;
        }
    }
}
