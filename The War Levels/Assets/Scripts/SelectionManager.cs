using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private bool isDragging = false;
    private bool selecting = false;
    private Vector3 mousePosition;//May need to be called sommething like lastMousePosition
    private NArmy[] narmies;
    private ArrayList selectedNarmyIndicies = new ArrayList();

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

    /* When the mouse button comes up find out what of all the selected armies within the box are.
     * 
     * Make a group of narmies to select.
     * See if they're in the bounds.
     * If one is in the bounds unselect all and select the ones in bounds.
     * 
     * This is to make sure the narmies are being selected and unseleccted correctly.
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//Getting initial mouse position
        {
            mousePosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            narmies = GetComponentsInChildren<NArmy>();
            for (int i = 0; i < narmies.Length; i++)
            {
                if (isWithinSelectionBounds(narmies[i].transform))
                {
                    selecting = true;
                    selectedNarmyIndicies.Add(i);
                }
            }

            if (selecting)
            {
                narmies[0].UnselectAll();
                foreach (int i in selectedNarmyIndicies)  narmies[i].SelectSelf();

                selectedNarmyIndicies = new ArrayList();
                selecting = false;
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
