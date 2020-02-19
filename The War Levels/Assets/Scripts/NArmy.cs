using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NArmy : Army
{
    public TextManager tManage;
    public NArmy[] otherNarmies;
    [HideInInspector] public bool selected;

    private static int armNum;//How many armies are on the field

    protected override void Start()
    {
        armNum++;
        base.Start();
    }

    protected override void Die()
    {
        armNum--;
        if (armNum < 1)//If it's the last army
        {
            tManage.MormonsLament();//Share the last message
            GameObject.Find("Timer").SendMessage("Finish");
        }
        base.Die();//Do the base death functions
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
