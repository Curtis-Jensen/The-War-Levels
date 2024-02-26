using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false;

    /* If the game starts off in a menu the game needs to be paused in the beginning.
     */
    void Start()
    {
        Pause();
    }

    /* Changes how fast time moves and marks whether the game is paused or not.
     */
    public void Pause()
    {
        if (isPaused)  Time.timeScale = 1f;
        else           Time.timeScale = 0f;
        
        isPaused = !isPaused;
    }
}
