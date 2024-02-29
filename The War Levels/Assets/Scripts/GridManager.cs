using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public Tile tilePrefab;
    public Transform cam;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity, transform);
                spawnedTile.name = $"Tile ({x},{y})";

                var evenHorizontal = x % 2 == 0;
                var evenVertical =   y % 2 == 0;
                var isOffset = (evenHorizontal && !evenVertical) || (!evenHorizontal && evenVertical);
                spawnedTile.SetColor(isOffset);
            }
        }

        cam.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
