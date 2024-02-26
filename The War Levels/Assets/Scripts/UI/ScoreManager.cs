using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int score; //Represents the score of of the current game being played
    public int highScore;
    //public Text scoreText;
    //public Text highScoreText; //Represents the highest score that the player has acchived in a single game

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("score");
        //score.text = "0";
        PlayerPrefs.DeleteKey("HighScore");
        //highScore.text = "0";
    }

    public static void updateHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    //I am pretty sure this method will not be needed
    //If it is needed the code almost definately needs to be changed
    public static void updateTotalMoney(int score)
    {
        score = score + PlayerPrefs.GetInt("score", 0);
        PlayerPrefs.SetInt("score", score);
    }

    

    
}

