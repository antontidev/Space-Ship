using MyBox;
using System;

[Serializable]
public class PerkTreasure : Perks
{
    [ReadOnly]
    public float price = 1f;

    public override float GetPrice()
    {
        return price;
    }
}

class TreasurePart : ItemPart<PerkTreasure>
{
}
