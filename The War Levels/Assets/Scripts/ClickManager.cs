using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public NArmy armySubject;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && armySubject.selected)
        {
            Vector3 clickPosition = -Vector3.one;

            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));

            transform.position = clickPosition;//By changing this object's position, the 
            

        }
    }
}
