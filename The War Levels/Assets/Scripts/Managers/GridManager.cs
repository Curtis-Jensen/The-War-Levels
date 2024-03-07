using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    public int width, height;
    public Tile grassTile;
    public Tile mountainTile;
    public Transform cam;

    void Awake()
    {
        instance = this;
    }

    public void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile randomTile = grassTile;

                var rand = Random.Range(0, 6);
                if (rand == 5)
                    randomTile = mountainTile;

                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity, transform);
                var type = Regex.Replace(randomTile.GetType().Name, "(\\B[A-Z])", " $1");
                spawnedTile.name = $"{type} ({x},{y})";

                var evenHorizontal = x % 2 == 0;
                var evenVertical =   y % 2 == 0;
                var isOffset = (evenHorizontal && !evenVertical) || (!evenHorizontal && evenVertical);
                spawnedTile.SetColor(isOffset);
            }
        }

        cam.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
