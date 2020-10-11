using System;

public enum AstronautRarity : int
{
    Normal,
    Rare,
    Legendary
}

[Serializable]
public class PerkAstronaut : Perks
{
    public AstronautRarity astronautRarity;

    public override float GetPrice()
    {
        return (int)astronautRarity;
    }
}

public class AstronautPart : ItemPart<PerkAstronaut>
{
}
