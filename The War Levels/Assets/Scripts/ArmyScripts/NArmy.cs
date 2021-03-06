﻿using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NArmy : Army
{
    public GameObject spear;
    public float spear_speed = 100f;
    public static NArmy[] nArmies;

    [HideInInspector] public bool selected;
    private static int armyNum;//How many armies are on the field
    private Vector3 clickPosition;
    private TextManager textManager;
    private SpriteRenderer flagSprite;
    private GameObject projectileHolder;

    /* So the armies are more visable during editing.
    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    /* In order to properly know when all Nephites have died at the end
     * all the Nephite armies need to be counted at the beginning
     * 
     * Gets NArmy specific data from Managers
     * Creates information used for changing the flag sprite.
     */
    protected override void Start()
    {
        armyNum++;
        GenerateNarmies();

        base.Start();
        textManager = data.tManage;
        projectileHolder = data.projectileHolder;

        flagSprite = GetComponent<AIDestinationSetter>().target.GetComponent<SpriteRenderer>();
    }

    public void GenerateNarmies()
    {
        nArmies = gameObject.transform.parent.GetComponentsInChildren<NArmy>();
    }

    /* Checks to see if certain buttons have been pressed.
     */
    private void Update()
    {
        if(selected)
        {
            if(Input.GetKeyUp("q"))
            {
                Fire_projectile("Spear");
            }
        }
    }

    /* saves the position of the mouse's click in the world's plane
     * instantiates a new spear in the same location as this object
     * creates a line from click position to this objects position
     * new_spear ignores collision with the army it's fired from
     */
    internal void Fire_projectile(string message)
    {
       if (message == "Spear")
       {
            clickPosition = GetWorldPositionOnPlane(Input.mousePosition, 0);
            Vector2 direction = (clickPosition - transform.position).normalized;
            GameObject new_spear = 
                Instantiate(spear, transform.position, Quaternion.identity, projectileHolder.transform);
            Physics2D.IgnoreCollision(new_spear.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            new_spear.transform.right = direction;
            new_spear.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(direction.x * spear_speed, direction.y * spear_speed);
       }
    }

    /* Assists in clicking when isometric.
     */
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    /* Change the appearance to make it look like it's selected.
     */
    private void Highlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    /* Change the appearance to make it look like it's normal.
    */
    private void Unhighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    /* Highlights, then makes sure that the navigation scripts know which NArmy is the important one.
     * 
     * Toggle the sprite renderer on the clickmanagers as well.
     * 
     * If we run into performance issues this could be optimized to deselect the old army through caching.
     */
    private void OnMouseDown()
    {
        UnselectAll();
        SelectSelf();
    }

    /* Marks the narmy as the one being commanded.
     * Makes flag opaque.
     */
    public void SelectSelf()
    {
        Highlight();
        flagSprite.color = new Color(1f, 1f, 1f, 1f);
        selected = true;
    }

    /* "At ease Narmies!!"
     */
    public void UnselectAll()
    {
        foreach (NArmy narmy in nArmies)
        {
            if (narmy == null)
            {
                GenerateNarmies();
                UnselectAll();
                break;
            }
            if (narmy.selected)
            {
                narmy.Unhighlight();
                narmy.selected = false;
                narmy.flagSprite.color = new Color(1f, 1f, 1f, .2f);
            }
        }
    }

    /* When the last army has died bring up the end screen.
     */
    protected override void Die()
    {
        armyNum--;
        if (armyNum < 1) textManager.MormonsLament();
        Destroy(flagSprite.gameObject);

        base.Die();
    }
}
