using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Faction faction;
    public BaseUnit unitPrefab;
}

public enum Faction
{
    Nephite = 0,
    Lamanite = 1
}
