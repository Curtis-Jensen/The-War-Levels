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
     */
    void Update()
    {
        transform.position = possibleTargets[DetermineShortestDistance()].position;
    }

    /* Checks to see which enemy army is closest to the army the script is attached to.
     */
    int DetermineShortestDistance()
    {
        int theSmallestOne = 0;
        for (int i = 1; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i] != null && possibleTargets[i - 1] != null) 
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
