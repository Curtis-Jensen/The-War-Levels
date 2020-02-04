using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Army : MonoBehaviour
{
    public float maxAttTirmr;
    public int soldNum;
    public int dmg;

    private float attTimr;
    private bool activated;

    protected virtual void Start()
    {
        Shrink(-soldNum);//So that at the start the army is appropriately sized
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if ((other.transform.tag == "Nephites" && this.transform.tag == "Lamanites") || (other.transform.tag == "Lamanites" && this.transform.tag == "Nephites"))
        {
            attTimr -= Time.deltaTime;
            if (attTimr < 0)
            {
                Battle();
            }
        }        
    }

    bool Flank()
    {
        return false;
        //return true if LArmy.numobjects > NArmy.numobjects
    }

    void Battle()
    {
        //soldNum -= (int)(dmg * Mathf.Exp(1.1f)*soldNum);//These exponents are too powerful.
        soldNum -= dmg;
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
}
