
/// <summary>
/// Base class for all type of items
/// </summary>
public abstract class Perks
{
    /// <summary>
    /// Choose if givent Perk is better than this
    /// </summary>
    /// <param name="another"></param>
    /// <returns></returns>
    public bool IsBetter(Perks another)
    {
        var anotherPrice = another.GetPrice();

        var currentPrice = GetPrice();

        return anotherPrice > currentPrice;
    }

    /// <summary>
    /// Calculates price of perk
    /// </summary>
    /// <returns></returns>
    public abstract float GetPrice();
}
