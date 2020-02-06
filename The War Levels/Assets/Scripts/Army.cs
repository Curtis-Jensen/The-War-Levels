using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Army : MonoBehaviour
{
    public float maxAttTirmr;
    public int soldNum;
    public int dmg;
    public Army armyInteract;
    public int adjacentArmies;

    //Unity API sysdocs


    private float attTimr;

    protected virtual void Start()
    {
        Shrink(-soldNum);//So that at the start the army is appropriately sized
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        armyInteract.adjacentArmies++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        armyInteract.adjacentArmies--;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if ((other.transform.tag == "Nephites" && this.transform.tag == "Lamanites") || (other.transform.tag == "Lamanites" && this.transform.tag == "Nephites"))
        {
           armyInteract = other.transform.GetComponent<Army>();

            attTimr -= Time.deltaTime;
            if (attTimr < 0)
            {
                Battle();
            }
        }
    }


    void Battle()
    {
        if (armyInteract.adjacentArmies > 1) { soldNum -= (int)(dmg * (1.5f)); }
        else soldNum -= dmg;

        Shrink(dmg);
        attTimr = maxAttTirmr;
        if (soldNum < 1)
        {
            Die();
        }
    }

    void Shrink(int dmg)
    {
        Vector3 theScale = transform.localScale;//Makes the vector to shrink with
        theScale.x -= (.0005f * (float)dmg);
        theScale.y -= (.0005f * (float)dmg);
        transform.localScale = theScale;//Applies the vector
    }

    void OnMouseDown()
    {

        Debug.Log(name);
    }
}
