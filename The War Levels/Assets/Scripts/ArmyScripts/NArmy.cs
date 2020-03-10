﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NArmy : Army
{
    public TextManager tManage;
    public GameObject spear;
    [HideInInspector] public bool selected;

    public static NArmy[] narmies;
    private static int armNum;//How many armies are on the field

    /* In order to properly know when all Nephites have died at the end
     * all the Nephite armies need to be counted at the beginning
     */
    protected override void Start()
    {
        armNum++;
        GenerateNarmies();
        base.Start();
    }

    public void GenerateNarmies()
    {
        narmies = gameObject.transform.parent.GetComponentsInChildren<NArmy>();
    }

    /* Checks to see if certain buttons have been pressed.
     */
    private void Update()
    {
        if(selected)
        {
            if(Input.GetKeyUp("q"))
            {
                fire_projectile("Spear");
            }
        }
    }

    /* Make the projectile appear,
     * Make sure collisions with the player don't count,
     * Make the projectile move somewhere.
     */
    internal void fire_projectile(string v)
    {
       if (v == "Spear")
        {
            GameObject new_spear = Instantiate(spear, transform.position, Quaternion.identity, transform.parent);
        }
    }

    /* Change the appearance to make it look like it's selected.
     */
    private void highlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    /* Change the appearance to make it look like it's normal.
    */
    private void unhighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    /* Hilights, then makes sure that the navigation scripts know which NArmy is the important one.
     * 
     * If we run into performance issues this could be optimized to only deselect the old army and not eveyone.
     */
    private void OnMouseDown()
    {
        foreach (NArmy narmy in narmies) { 
        
            if(narmy == null)
            {
                GenerateNarmies();
                OnMouseDown();
                break;
            }
            narmy.unhighlight();
            narmy.selected = false;
        }
        selected = true;
        highlight();
    }

    /* When the last army has died bring up the end screen.
     * Restructure the narmies but not somehow after we're dead...
     */
    protected override void Die()
    {
        armNum--;
        if (armNum < 1)
        {
            tManage.MormonsLament();
        }

        base.Die();
    }
}
