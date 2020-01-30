using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NArmy : Army
{
    public TextManager tManage;

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
        }
        base.Die();//Do the base death functions
    }

    void OnClick() //When clicked on, it becomes highlighted and can be moved with other clicks.
    {

    }
}
