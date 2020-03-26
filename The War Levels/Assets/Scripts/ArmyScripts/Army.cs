using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class Army : MonoBehaviour
{
    public int soldierNumber;//How many soldiers the army has

    private int baseDamage;//How much damage is done before flanking and other modifiers are applied.
    private int flankingDefense;//How much defense is gained from being attacked by an army the army is gocussed on.
    private int adjacentArmies;
    private int totalDamage;//However much damage actually gets applied to the army.
    private int otherArmysSoldiers;
    private float maxAttackTimer;
    private float attackTimer;
    private string myTag;
    private string otherTag;
    private AIDestinationSetter nav;

    protected ArmoryDataHolder data;

    /* Calls the oposite of shrink to expand the armies to what they need to be.
     * 
     * Gets the data from ArmoryDataHolder.
     */
    protected virtual void Start()
    {
        data = GameObject.Find("Managers").GetComponent<ArmoryDataHolder>();
        baseDamage      = data.baseDamage;
        flankingDefense = data.flankingDefense;
        maxAttackTimer  = data.maxAttackTimer;
        nav = GetComponent<AIDestinationSetter>();

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

    /* Called every frame of a collision.
     * 
     * When a collision with an enemy has happened long enough they go through battle calculations.
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsEnemy(other))  otherArmysSoldiers = other.transform.GetComponent<Army>().soldierNumber;

        if (other.transform.tag == "Projectile" && gameObject.tag == "Lamanites")
        {
            Damage_unit(totalDamage, 2);
            Destroy(other.gameObject);
        }
    }
    /**
    * Damages unit by calling shrink and changing soilder number at the same time CALL THIS IF YOU NEED TO DAMAGE SOMETHING.
    * 
    **/
    void Damage_unit(int number)
    {
        Shrink(totalDamage);
        soldierNumber -= totalDamage;
    }
    /**
    * Damages unit by calling shrink and changing soilder number at the same time CALL THIS IF YOU NEED TO DAMAGE SOMETHING.
    * 
    * Can also be multiplied by a number, if no multiplication is intended, just put 1 in the second parameter.
    **/
    void Damage_unit(int number, int multiplier)
    {
        Shrink(totalDamage * multiplier);
        soldierNumber -= totalDamage * multiplier;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (IsEnemy(other))
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0) BattleCalculations(other);
        }
    }

    /* Does the math to see how many men die in this army by:
     * 
     * Resetting the total damage from any previous modifiers,
     * 
     * Applying flanking effects by not taking as much damage if the enemy army is
     * being targeted,
     * 
     * Having bigger armies squash smaller armies...
     * 
     * Calling the functions that make the army shrink and die when they have no men.
     */
    void BattleCalculations(Collision2D other)
    {
        totalDamage = baseDamage;
        if ((int)other.transform.position.x == (int)nav.target.position.x)
        {
            totalDamage /= flankingDefense;
            Debug.Log(name + totalDamage);
        }

        soldierNumber -= totalDamage;
        Shrink(totalDamage);

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

    /* Destroys self.
    */
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
