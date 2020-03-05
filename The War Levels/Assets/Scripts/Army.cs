using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Army : MonoBehaviour
{
    public int soldierNumber;//How many soldiers the army has
    public Vector3 target;
    public ArmoryDataHolder data;

    private int baseDamage;//How much damage is done before flanking and other modifiers are applied.
    private int flankingDefense;//How much defense is gained from being attacked by an army the army is gocussed on.
    private int adjacentArmies;
    private int damage;//However much damage actually gets applied to the army.
    private int otherArmysSoldiers;
    private float maxAttackTimer;
    private float attackTimer;
    private string myTag;
    private string otherTag;

    /* Calls the oposite of shrink to expand the armies to what they need to be.
     * 
     * Gets the data from ArmoryDataHolder.
     */
    protected virtual void Start()
    {
        baseDamage      = data.baseDamage;
        flankingDefense = data.flankingDefense;
        maxAttackTimer  = data.maxAttackTimer;

        Shrink(-soldierNumber);//So that at the start the army is appropriately sized
    }

    /* Checks to see if the collision was with an enemy by comparing tags.
     */
    bool IsEnemy(Collision2D other)
    {
        myTag = this.transform.tag;
        otherTag = other.transform.tag;
        return (otherTag == "Nephites" && myTag == "Lamanites") || (otherTag == "Lamanites" && myTag == "Nephites");
    }

    /* Destroys self.
     */
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    /* Called every frame of a collision.
     * 
     * When a collision with an enemy has happened long enough they go through battle calculations.
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsEnemy(other))
        {
            otherArmysSoldiers = other.transform.GetComponent<Army>().soldierNumber;
            if(other.transform.position == target)
            {

            }
        }
        if (other.transform.tag == "Projectile" && gameObject.tag == "Lamanites")
        {
            damage_unit(damage, 2);
            Destroy(other.gameObject);
        }
    }
    /**
     * Damages unit by calling shrink and changing soilder number at the same time CALL THIS IF YOU NEED TO DAMAGE SOMETHING.
     * 
    **/
    void damage_unit(int number, int multiplier)
    {
        Shrink(damage * multiplier);
        soldierNumber -= damage * multiplier;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (IsEnemy(other))
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                Battle(other);
            }
        }
    }

    /* Does the math to see how many men die in this army,
     * 
     * Applies flanking effects by not taking as much damage if the enemy army is
     * being targeted...
     * 
     * Have bigger armies squash smaller armies...
     * 
     * Calls the functions that make the army shrink and die when they have no men.
     */
    void Battle(Collision2D other)
    {
        damage = baseDamage;
        otherArmysSoldiers = other.transform.GetComponent<Army>().soldierNumber;
        if (other.transform.position == target)
        {
            damage /= flankingDefense;
        }

        Debug.Log(name + damage);
        soldierNumber -= damage;
        Shrink(damage);

        attackTimer = maxAttackTimer;
        if (soldierNumber < 1)
        {
            Die();
        }
    }

    /* Changes the size of the army (typically when an army takes damage)
     * 
     * We should probably have this script change the amount of men the army has so we don't have duplicate lines of
     * code, but we'll optimize that later... >.>
     */
    public void Shrink(int damage)
    {
        Vector3 theScale = transform.localScale;//Makes the vector to shrink with
        theScale.x -= (.0005f * (float)damage);
        theScale.y -= (.0005f * (float)damage);
        transform.localScale = theScale;//Applies the vector
    }
}
