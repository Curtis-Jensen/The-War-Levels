using UnityEngine;
using System.Collections;

public class SharpText : MonoBehaviour {

    public float sizeInUnits; // Set size of text in units. Most fonts have a lot of empty space above and below the characters. So it won't probably match the grid.
    public TextMesh textMesh; //In inspector assign the Text Mesh component

    public Camera mainCamera;
    private float sharpness;
	
	// Update is called once per frame
	void Start() {

        OnCameraSizeChange();
	}

    public void OnCameraSizeChange()//call this function everytime you change camera size
    {
        sharpness = Screen.height / (20 * mainCamera.orthographicSize);
        textMesh.fontSize = Mathf.RoundToInt(sharpness * sizeInUnits);
        textMesh.characterSize = 1 / (float)sharpness;
    }
}
