using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
public class LArmy : Army
{
    public GameObject lamanite_army;
    public static NArmy[] narmies;

    private List<Transform> narmyTargets = new List<Transform>();
    private List<Transform> selectedNarmies = new List<Transform>();
    private Vector3 spawnPoint;
    private GameObject nephi;

    /* So the armies are more visable during editing.
    */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    /* Get acquainted with Nephi and his children to later target them.
     * 
     * Keep in mind the spawn point so that whoever replaces them for The Nephite's End goes there
     */
    protected override void Start()
    {
        nephi = GameObject.Find("Nephi");
        GenerateNarmies();

        spawnPoint = transform.position;
        base.Start();
    }

    public void GenerateNarmies()
    {
        narmies = nephi.GetComponentsInChildren<NArmy>();
    }

    /* Manage navigation for each Narmy targeting this larmy.
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
                    Input.GetMouseButtonDown(1) && selectedNarmies.Contains(narmyTargets[i]))
            {
                narmyTargets.RemoveAt(i);
                continue;
            }
            narmyTargets[i].position = transform.position;
        }
    }

    /* When the player left clicks on a LArmy the navigating NArmy will track this LArmy.
     * 
     * Adds the narmy's targeting to the list of enemies.
     */
    void OnMouseDown()
    {
        foreach(Transform selectedNarmy in FindSelectedNarmies())
        {
            narmyTargets.Add(selectedNarmy);
        }
    }

    /* Called every frame of a collision.
 * 
 * When a collision with an enemy has happened long enough they go through battle calculations.
 */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Projectile"))
        {
            Shrink(totalDamage);
            soldierNumber -= totalDamage;
            Destroy(other.gameObject);
        }
    }

    /* Looks throught the narmies to see which ones are selected
     */
    List<Transform> FindSelectedNarmies()
    {
        selectedNarmies = new List<Transform>();
        foreach (NArmy narmy in narmies)
        {
            if (narmy == null)
            {
                GenerateNarmies();
                FindSelectedNarmies();
                break;
            }
            if (narmy.selected)
                selectedNarmies.Add(GameObject.Find("Nav " + narmy.name).GetComponent<Transform>());
        }
        return selectedNarmies;
    }

    /* The Lamanites spawn new Lamanites when they die because they were "innumerable" in
     * the battle, so they are infinite in code.
     * 
     * Spawns a new Larmy where this one started.
     * 
     * Makes sure the name is correct in order to find the navigation manager.
     * 
     * Create it's own navigation manager and call FindTarget() on the new AIDestinationSetter.
     */
    protected override void Die()
    {
        GameObject lamanite = Instantiate(lamanite_army, spawnPoint,
            Quaternion.identity, transform.parent);

        lamanite.name = name;
        lamanite.GetComponent<AIDestinationSetter>().FindTarget();
        base.Die();
    }
}
