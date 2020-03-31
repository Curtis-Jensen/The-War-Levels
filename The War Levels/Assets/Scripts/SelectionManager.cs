using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 mousePosition;//May need to be called sommething like lastMousePosition
    private NArmy[] narmies;

    /* When The player clicks start to draw a rectangle for selection.
     * Any narmies that were selected get set as selected.
     * 
     * Any larmies that were selected get targeted by order of which is closest 
     * (LamaniteTargeter has a method for this)
     */
    void OnGUI()
    {
        if (isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(mousePosition, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(.8f, .8f, .95f, .1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.red);
        }
    }

    /* 
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            narmies = GetComponentsInChildren<NArmy>();
            narmies[0].UnselectAll();
            foreach (NArmy narmy in narmies)
            {
                if (isWithinSelectionBounds(narmy.transform)) narmy.SelectSelf();
            }
            isDragging = false;
        }
    }

    bool isWithinSelectionBounds(Transform transform)
    {
        if (!isDragging) return false;

        var camera = Camera.main;
        var viewportBounds = ScreenHelper.GetViewportBounds(camera, mousePosition, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(transform.position));
    }
}
