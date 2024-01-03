namespace Domain;

public class SwappingCommand
{
    public IReadOnlyCollection<SwapTrick> SwapTricks { get; }

    public SwappingCommand(IReadOnlyCollection<SwapTrick> swapTricks)
    {
        ArgumentNullException.ThrowIfNull(swapTricks);

        if (!swapTricks.Any())
        {
            throw new ArgumentException(
                "Swaps tricks count should be greater than zero.");
        }

        SwapTricks = swapTricks;
    }
}
