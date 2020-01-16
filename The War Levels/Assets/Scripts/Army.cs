using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Army : MonoBehaviour
{
    public float maxAttTirmr;
    public int soldNum;
    public int dmg;

    private float attTimr;

    protected virtual void Start()
    {
        Shrink(-soldNum);//So that at the start the army is appropriately sized
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay2D()
    {
        attTimr -= Time.deltaTime;

        if(attTimr < 0)
        {
            Battle();
        }
    }

    void Battle()
    {
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
