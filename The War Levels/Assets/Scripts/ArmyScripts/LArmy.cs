using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
public class LArmy : Army
{
    public GameObject lamanite_army;
    public static NArmy[] narmies;

    private List<Transform> narmyTargets = new List<Transform>();
    private GameObject nephi;

    /* So the armies are more visable during editing.
    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

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
        narmies = nephi.GetComponentsInChildren<NArmy>();
    }

    /* Manage navigation for each Narmy targeting th
     * 
     * If the player right clicks and the Narmy targeting is selected
     * set clickManager to null.
     * 
     * Help the selected NArmy attack this by continually changing the position of
     * the clickmanager.
     */
    void Update()
    {
        for (int i = 0; i < narmyTargets.Count; i++)
        {
            if (narmyTargets[i] == null || 
                    Input.GetMouseButtonDown(1) && narmyTargets[i] == FindSelectedNArmy())
            {
                narmyTargets.RemoveAt(i);
                continue;
            }
            narmyTargets[i].position = transform.position;
        }

        //if (Input.GetMouseButtonDown(1) && narmyTargets[0] == FindSelectedNArmy())
        //    narmyTargets[0] = null;
        
        //else if (narmyTargets[0] != null)
        //    narmyTargets[0] = transform.position;
    }

    /* When the player left clicks on a LArmy the navigating NArmy will track this LArmy.
     * 
     * Adds the narmy's targeting to the list of enemies.
     */
    void OnMouseDown()
    {
        narmyTargets.Add(FindSelectedNArmy());
    }

    Transform FindSelectedNArmy()
    {
        foreach (NArmy narmy in narmies)
        {
            if (narmy == null)
            {
                GenerateNarmies();
                FindSelectedNArmy();
                break;
            }
            if (narmy.selected)
                return GameObject.Find("Nav " + narmy.name).GetComponent<Transform>();
        }
        return null;
    }

    /* The Lamanites spawn new Lamanites when they die because they were "innumerable" in
     * the battle, so they are infinite in code.
     * 
     * Makes sure the name is correct for navigation purposes.
     */
    protected override void Die()
    {
        GameObject lamanite = Instantiate(lamanite_army, new Vector3(5.478f, -0.687f, 0), Quaternion.identity);
        lamanite.GetComponent<LArmy>().soldierNumber = 10000;
        lamanite.name = name;
        base.Die();
    }
}
