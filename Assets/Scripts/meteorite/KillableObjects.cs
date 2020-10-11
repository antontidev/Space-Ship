using System;
using System.Collections.Generic;
using UnityEngine;

public enum Killable
{
    Summon,
    Player
}

public class EnumLayerList<T>
{
    /// <summary>
    /// Holds layers of enum layer list
    /// </summary>
    public List<int> enumLayerList;

    public EnumLayerList()
    {
        enumLayerList = new List<int>();

        var names = Enum.GetNames(typeof(T));

        foreach(var name in names)
        {
            var layer = LayerMask.NameToLayer(name);

            enumLayerList.Add(layer);
        }
    }
}

public class KillableObjects : EnumLayerList<Killable>
{

}