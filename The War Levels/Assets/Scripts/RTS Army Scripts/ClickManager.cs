using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private NArmy armySubject;
    private Vector3 clickPosition;

    /* Finding the army subject by looking for it's name.
     * 
     * For instance, "Nav Narmy (1)" would look for "Narmy (1)".
     */
    void Start()
    {
        string armySubjectName = name.Substring(4);
        armySubject = GameObject.Find(armySubjectName).GetComponent<NArmy>();
        transform.position = armySubject.transform.position;
    }

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

    /* Makes up for the tilt of the camera
     */
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
