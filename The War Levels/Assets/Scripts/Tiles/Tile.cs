using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color baseColor, offsetColor;
    private SpriteRenderer renderer;

    public GameObject highlight;

    void Awake()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetColor(bool isOffset)
    {
        if (isOffset)
            renderer.color = offsetColor;
        else
            renderer.color = baseColor;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }    
    
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
