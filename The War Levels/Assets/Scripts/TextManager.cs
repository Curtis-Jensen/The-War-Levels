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

    /* Just calls TextToggle() for closing.  Important code occurs in TextToggle().
     * But it does reset the position of the scrollbar so it's not on the bottom next time it opens.
     */
    public void CloseText()//Closes text
    {
        scrollbar.value = 1f;
        TextToggle(false);
    }

    /* Opposite of above.
     */
    public void OpenText()//Opens text
    {
        TextToggle(true);
    }

    /* Toggles the activity of the text objects,
     * resets the position of the scrollbar
     */
    void TextToggle(bool opening)//Where the actual code of close and open text take place
    {
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