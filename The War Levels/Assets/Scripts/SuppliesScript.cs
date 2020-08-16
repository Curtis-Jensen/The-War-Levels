using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesScript : MonoBehaviour
{
    public TextManager textManager;

    /* When a Larmy collides start reducing health, when destroyed call the end.
     * 
     * Make sure the collisions aren't coming from the Nephites.
     * Inherits the shrinking function from Army.
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Nephites")  textManager.MormonsLament();
    }
}
