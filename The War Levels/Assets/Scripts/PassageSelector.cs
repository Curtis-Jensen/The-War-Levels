using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageSelector : MonoBehaviour
{
    [TextArea(2, 1000)]//Allows the developer to type in a comfier box (has a glitch with the scrollbar
    public string[] passages;

    /* Output a random passage, or verse of scripture, from an array of fitting passages to be used during a story beat.
     * 
     * First the a next button or triggering event will call this script and decide
     * which collection of pasages to choose from,
     * then it will choose a random one of those passages to send
     */
    public string SelectPassage()
    {
        return passages[Random.Range(0, passages.Length + 1)];
    }
}
