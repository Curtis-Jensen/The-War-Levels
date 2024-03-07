using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitManager instance;

    void Awake()
    {
        instance = this;
    }
}
