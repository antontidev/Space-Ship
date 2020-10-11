using UnityEngine;

/// <summary>
/// Base class for type of items you can peek
/// </summary>
public abstract class ItemPart<T> : MonoBehaviour where T : Perks, new()
{
    [SerializeField]
    private T perks;

    public ItemPart()
    {
        perks = new T();
    }

    public virtual bool IsBetter(ItemPart<T> another)
    {
        var price = GetPrice();

        var anotherPrice = another.GetPrice();

        return anotherPrice > price;
    }

    public virtual float GetPrice()
    {
        return perks.GetPrice();
    }
}
