using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NArmy : Army
{
    public GameObject spear;
    public float spear_speed = 100f;

    [HideInInspector] public bool selected;
    private static int armNum;//How many armies are on the field
    private Vector3 clickPosition;
    public static NArmy[] nArmies;
    private TextManager tManage;

    /* In order to properly know when all Nephites have died at the end
     * all the Nephite armies need to be counted at the beginning
     * 
     * Gets NArmy specific data from Managers
     */
    protected override void Start()
    {
        //Fire_point_spawner();
        armNum++;
        GenerateNarmies();
        base.Start();
        tManage = data.tManage;
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

    /* Make the projectile appear,
     * Make sure collisions with the player don't count,
     * Make the projectile move somewhere.
     */
    internal void Fire_projectile(string message)
    {
       if (message == "Spear")
        {
            //saves the position of the mouse's click in the world's plane
            clickPosition = GetWorldPositionOnPlane(Input.mousePosition, 0);
            //creates a line from click position to this objects position
            Vector2 direction = (clickPosition - transform.position).normalized;
            //instantiates a new spear in the same location as the this object
            GameObject new_spear = Instantiate(spear, transform.position, Quaternion.identity, transform.parent);
            //new_spear ignores collision 
            Physics2D.IgnoreCollision(new_spear.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            new_spear.transform.right = direction;
            new_spear.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * spear_speed, direction.y * spear_speed);

        }
    }
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
     * If we run into performance issues this could be optimized to only deselect the old army and not eveyone.
     */
    private void OnMouseDown()
    {
        foreach (NArmy narmy in nArmies) { 
        
            if(narmy == null)
            {
                GenerateNarmies();
                OnMouseDown();
                break;
            }
            narmy.Unhighlight();
            narmy.selected = false;
        }
        selected = true;
        Highlight();
    }

    /* So the armies are more visable during editing.
    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one);
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
