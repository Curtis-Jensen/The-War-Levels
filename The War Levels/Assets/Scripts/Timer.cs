using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    private float startTime;
    private bool finished = false;
    private float t;
    //public ScoreManager scoreManager = new ScoreManager();

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(finished)
        {
            return;
        }
        else
        {
            t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
       
    }

    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow;
        Debug.Log("Pre High score" + PlayerPrefs.GetInt("highScore", 0));
        ScoreManager.updateHighScore((int)t);
        Debug.Log("High score" + (int)t);
        Debug.Log("Post High score" + PlayerPrefs.GetInt("highScore", 0));
    }
}
