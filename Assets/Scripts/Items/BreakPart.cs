using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PerkBreak : Perks
{
    public float price;

    public override float GetPrice()
    {
        return price;
    }
}

/// <summary>
/// Class for trash and it's price, perks and so
/// </summary>
public class BreakPart : ItemPart
{
    public PerkBreak perkBreak;

    public override float GetPrice()
    {
        return perkBreak.GetPrice();
    }
}