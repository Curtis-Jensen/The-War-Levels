using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    public Text score; //Represents the score of of the current game being played
    public Text highScore; //Represents the highest score that the player has acchived in a single game

    // Start is called before the first frame update
    void Start()
    {
        score.text = PlayerPrefs.GetInt("score", 0).ToString();
        highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();
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
        score.text = "0";
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }

    public static void UpdateHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    //I am pretty sure this method will not be needed
    //If it is needed the code almost definately needs to be changed
    public static void UpdateTotalMoney(int score)
    {
        score = score + PlayerPrefs.GetInt("score", 0);
        PlayerPrefs.SetInt("score", score);
    }

    

    
}

