﻿using MyBox;
using System;

[Serializable]
public class PerkRock : Perks
{
    [ReadOnly]
    public float price = 1f;

    public override float GetPrice()
    {
        return price;
    }
}

/// <summary>
/// Class with logic of Rocks and it's price
/// </summary>
class RockPart : ItemPart<PerkRock>
{
}
