using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesScript : MonoBehaviour
{
    public TextManager textManager;

    /* When a Larmy collides start reducing health, when destroyed call the end.
     * 
     * Make sure the collisions aren't coming from the Nephites.
     * Could inherit some things from battle calculations from Army.cs???
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag != "Nephites")  textManager.MormonsLament();
    }
}
