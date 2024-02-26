using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject textHolder;
    public GameObject aICTPbutton;
    public PauseManager pauser;
    public Scrollbar scrollbar;
    [TextArea(2, 20)]
    public string outroText;

    /* Toggles the activity of the text objects,
     * resets the position of the scrollbar
     */
    public void TextToggle(bool opening)
    {
        scrollbar.value = 1f;
        panel.SetActive(opening);
        aICTPbutton.SetActive(opening);
        pauser.Pause();
    }

    public void MormonsLament()//Activates the final message
    {
        textHolder.GetComponent<Text>().text = gameObject.GetComponentInParent<PassageSelector>().SelectPassage();
        aICTPbutton.GetComponentInChildren<Text>().text = "THE END";//Button says the end
        TextToggle(true);//Turns on the text box
    }
}