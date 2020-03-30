using UnityEngine;
using System.Collections;

public class SharpText_Demo : MonoBehaviour {

    //This script showcases what SharpTextMesh does.
    //It updates every frame to adjust to changing  the camera size in editor. Therefore it slows down runtime performance.
    //For normal usage use SharpText.cs

    public float sizeInUnits; // Set size of text in units. Most fonts have a lot of empty space above and below the characters. So it won't probably match the grid.
    public TextMesh textMesh; //In inspector assign the Text Mesh component
    public Camera mainCamera;

    private float sharpness;
	
	// Update is called once per frame
	void Update() {

        sharpness = Screen.height / (20 * mainCamera.orthographicSize);
        textMesh.fontSize = Mathf.RoundToInt(sharpness*sizeInUnits);
        textMesh.characterSize = 1/(float)sharpness;

	}
}
