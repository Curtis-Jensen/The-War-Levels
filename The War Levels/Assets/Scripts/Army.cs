using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Army : MonoBehaviour
{
    public float maxAttackTimer;
    public int soldierNumber;
    public int damage;

    private int adjacentArmies;
    private string myTag;
    private string otherTag;
    private int otherArmysSoldiers;
    private float attackTimer;

    bool IsEnemy(Collision2D other)
    {
        myTag = this.transform.tag;
        otherTag = other.transform.tag;
        return (otherTag == "Nephites" && myTag == "Lamanites") || (otherTag == "Lamanites" && myTag == "Nephites");
    }

    protected virtual void Start()
    {
        Shrink(-soldierNumber);//So that at the start the army is appropriately sized
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsEnemy(other))
        {
            otherArmysSoldiers = other.transform.GetComponent<Army>().soldierNumber;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (IsEnemy(other))
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                Battle();
            }
        }
    }

    void Battle()
    {
        damage = otherArmysSoldiers / soldierNumber * 100;
        Debug.Log(name + damage);
        soldierNumber -= damage;
        Shrink(damage);
        attackTimer = maxAttackTimer;
        if (soldierNumber < 1)
        {
            Die();
        }
    }

    void Shrink(int damage)
    {
        Vector3 theScale = transform.localScale;//Makes the vector to shrink with
        theScale.x -= (.0005f * (float)damage);
        theScale.y -= (.0005f * (float)damage);
        transform.localScale = theScale;//Applies the vector
    }
}
