using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LArmy : Army
{
public GameObject lamanite_army;
    protected override void Die()
    {
    GameObject lamanite = Instantiate(lamanite_army, new Vector3(5.478f, -0.687f, 0), Quaternion.identity);
        lamanite.GetComponent<LArmy>().Shrink(-10000);
        base.Die();
    }


}
