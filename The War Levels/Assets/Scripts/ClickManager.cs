using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public NArmy armySubject;

    private Vector3 clickPosition;

    /* When the player clicks this object's location goes there.
     * When this object's location goes there it makes the army follow.
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && armySubject.selected)
        {
            clickPosition = GetWorldPositionOnPlane(Input.mousePosition, 0);

            transform.position = clickPosition; 
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
