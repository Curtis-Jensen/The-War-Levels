using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject textHolder;
    public GameObject aICTPbutton;
    public Scrollbar scrollbar;
    [TextArea(3, 10)]
    public string outroText;

    /*
     */
    public void CloseText()//Closes text
    {
        TextToggle(false);
    }

    /* See above.
     */
    public void OpenText()//Opens text
    {
        TextToggle(true);
    }

    void TextToggle(bool opening)//Where the actual code of close and open text take place
    {
        scrollbar.value = 1f;//Trying to reset the scrollbar's position when the window reopens
        panel.SetActive(opening);
        aICTPbutton.SetActive(opening);
    }

    public void MormonsLament()//Activates the final message
    {
        textHolder.GetComponent<Text>().text = outroText;//Makes the message appropriate
        aICTPbutton.GetComponentInChildren<Text>().text = "THE END";//Button says the end
        TextToggle(true);//Turns on the text box
    }
}