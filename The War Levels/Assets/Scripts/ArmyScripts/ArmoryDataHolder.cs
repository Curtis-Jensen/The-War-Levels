using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ArmoryDataHolder : MonoBehaviour
{
    /* This class exists to make it easier to set numbers for multiple armies at once.
     */
    public int baseDamage;
    public int flankingDefense;//How much defense is gained from being attacked by an army the army is gocussed on.
    public float maxAttackTimer;
    public float armySpeed;
    public TextManager tManage;
    public AIPath[] navigators;
    public GameObject projectileHolder;

    /* Sets the speed and acceleration for all armies.
     */
    void Start()
    {
        foreach (AIPath navigator in navigators)
        {
            if(navigator != null)  navigator.maxSpeed = armySpeed;
        }
    }
}
