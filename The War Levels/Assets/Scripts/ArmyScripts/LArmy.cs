using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LArmy : Army
{
    public GameObject lamanite_army;
    public static NArmy[] nArmies;

    private Transform clickManager;
    private GameObject nephi;

    /* Get acquainted with Nephi and his children to later be targeted by them.
     */
    protected override void Start()
    {
        nephi = GameObject.Find("Nephi");
        GenerateNarmies();
        base.Start();
    }

    public void GenerateNarmies()
    {
        nArmies = nephi.GetComponentsInChildren<NArmy>();
    }

    /* Help the selected NArmy attack this.
     * Also, if the Narmy decides to go somewhere else unselect this LArmy.
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            clickManager = null;
        }
        else if (clickManager != null)
        {
            clickManager.position = transform.position;
        }
    }

    /* When the player left clicks on a LArmy the navigating NArmy will track this LArmy.
     * 
     * Make sure nothing is null,
     * Find which NArmy is selected,
     * find it's click manager,
     * set it as the click manager to be moved.
     */
    void OnMouseDown()
    {
        foreach (NArmy nArmy in nArmies)
        {
            if (nArmy == null)
            {
                GenerateNarmies();
                OnMouseDown();
                break;
            }
            if (nArmy.selected)
            {
                clickManager = GameObject.Find("Nav " + nArmy.name).GetComponent<Transform>();
                break;
            }
        }
    }

    /* So the armies are more visable during editing.
     */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    /* The Lamanites spawn new Lamanites when they die because they were "innumerable" in
     * the battle, so they are infinite in code.
     */
    protected override void Die()
    {
        GameObject lamanite = Instantiate(lamanite_army, new Vector3(5.478f, -0.687f, 0), Quaternion.identity);
        lamanite.GetComponent<LArmy>().soldierNumber = 10000;
        base.Die();
    }
}
