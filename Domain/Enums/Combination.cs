namespace Domain.Enums;

/// <summary>
/// Комбинация среди карт в рукаве (по умолчанию 5 штук).
/// </summary>
public enum Combination : byte
{
    /// <summary>
    /// Старшая карта.
    /// </summary>
    SeniorCard = 0,

    /// <summary>
    /// Двойка.
    /// </summary>
    Couple,

    /// <summary>
    /// Тройка.
    /// </summary>
    Triple,

    /// <summary>
    /// Две пары.
    /// </summary>
    DoubleCouple,

    /// <summary>
    /// Все карты идут по порядку.
    /// </summary>
    Street,

    /// <summary>
    /// Все карты одной масти.
    /// </summary>
    Flash,

    /// <summary>
    /// Двойка + тройка.
    /// </summary>
    FullHouse,

    /// <summary>
    /// Четыре карты.
    /// </summary>
    Kare,

    /// <summary>
    /// Все карты по порядку одной масти.
    /// </summary>
    StreetFlash,

    /// <summary>
    /// <see cref="StreetFlash"/>
    /// начиная с <see cref="CardType.Ten"/>.
    /// </summary>
    RoyalFlash
}
