using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NArmy : Army
{
    public Timer timer;
    public TextManager tManage;
    public NArmy[] otherNarmies;
    public GameObject spear;
    [HideInInspector] public bool selected;

    private static int armNum;//How many armies are on the field

    protected override void Start()
    {
        armNum++;
        base.Start();
    }
    private void Update()
    {
        if(selected)
        {
            if(Input.GetKeyUp("q"))
            {
                fire_projectile("Spear");
            }
        }
    }

    protected override void Die()
    {
        armNum--;
        if (armNum < 1)//If it's the last army
        {
            timer.Finish();
            tManage.MormonsLament();//Share the last message
        }
        base.Die();//Do the base death functions
    }

    internal void fire_projectile(string v)
    {
       if (v == "Spear")
        {
            Instantiate(spear, transform.position, Quaternion.identity, transform.parent);
        }
    }

    private void highlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void unhighlight()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    void OnMouseDown()
    {
        highlight();

        selected = true;
        foreach (NArmy other in otherNarmies)
        {
            other.unhighlight();
            other.selected = false;
        }
    }
}
