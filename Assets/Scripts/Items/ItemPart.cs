using UnityEngine;

/// <summary>
/// Base class for type of items you can peek
/// </summary>
public abstract class ItemPart : MonoBehaviour
{
    public virtual bool IsBetter(ItemPart another)
    {
        var price = GetPrice();

        var anotherPrice = another.GetPrice();

        return anotherPrice > price;
    }

    public abstract float GetPrice();
}
