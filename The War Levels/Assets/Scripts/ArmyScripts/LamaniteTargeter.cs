using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamaniteTargeter : MonoBehaviour
{
    public Transform myTransform;

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
     * The Nephite armies need to be reaccounted for when one of them dies.
     */
    int DetermineShortestDistance()
    {
        int theSmallestOne = 1;
        if (possibleTargets.Length > 1)
        {
            for (int i = 1; i < possibleTargets.Length; i++)
            {
                if (Vector3.Distance(possibleTargets[i].position, transform.position) <
                    Vector3.Distance(possibleTargets[i - 1].position, transform.position) &&
                    possibleTargets[i] != null)
                {
                    theSmallestOne = i;
                }
            }
        }
        return theSmallestOne;
    }

    /* Called at start or when an army is killed or created. 
     */
    public void FillPossibleTargets()
    {
        possibleTargets = gameObject.transform.GetComponentInParent<NavDataHolder>()
            .nephites.GetComponentsInChildren<Transform>();
    }
}
