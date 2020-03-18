using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamaniteTargeter : MonoBehaviour
{
    public Transform armySubject;

    static Transform[] possibleTargets;

    /* Gets the manager for the Nephites via the navigation manager to find the enemy.
     */
    void Start()
    {
        FillPossibleTargets();
    }

    /* Changes the location of this targeter based on where the closest army is.
     * But first checks to see if anyone has died, and if so, gets reacquanted with the surroundings.
     */
    void Update()
    {
        foreach(Transform transform in possibleTargets)
        {
            if(transform == null)
            {
                FillPossibleTargets();
            }
        }
        transform.position = possibleTargets[DetermineShortestDistance()].position;
    }

    /* Checks to see which enemy army is closest to the army the script is attached to.
     * 
     * If there is only one Narmy so that no comarisons can take place this should return 1.
     * 
     * Makes them go to (0,0) (The Nephite's parent) if broken or there are no NArmies to chase.
     * 
     * The index of the for loop should start at 2 because the 0 index is the parent of the
     * Nephites, which should not be considered a viable target.
     */
    int DetermineShortestDistance()
    {
        int theClosestIndex = 0;
        if (possibleTargets.Length > 2)
        {
            for (int i = 2; i < possibleTargets.Length; i++)
            {
                if (Vector3.Distance(possibleTargets[i].position, transform.position) <
                    Vector3.Distance(possibleTargets[i - 1].position, transform.position) &&
                    possibleTargets[i] != null)
                {
                    theClosestIndex = i;
                }
            }
        }
        else if (possibleTargets.Length > 1)
        {
            theClosestIndex = 1;
        }

        return theClosestIndex;
    }

    /* Called at start or when an army is killed or created. 
     */
    public void FillPossibleTargets()
    {
        possibleTargets = gameObject.transform.GetComponentInParent<NavDataHolder>()
            .nephites.GetComponentsInChildren<Transform>();
    }
}
