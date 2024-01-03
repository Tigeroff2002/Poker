using Domain.Enums;

namespace Domain.Combinations;

public abstract class Combination
{
    public CombinationType CombinationType { get; }

    protected Combination(CombinationType combinationType)
    {
        CombinationType = combinationType;
    }
}

