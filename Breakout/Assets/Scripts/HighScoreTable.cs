using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class HighScoreTable : MonoBehaviour
{

    public Text score1Text;
    public Text score2Text;
    public Text score3Text;
    public Text score4Text;
    public Text score5Text;
    public Text name1Text;
    public Text name2Text;
    public Text name3Text;
    public Text name4Text;
    public Text name5Text;

    void Start(){
        

        if (!PlayerPrefs.HasKey("HighScore1")){
            PlayerPrefs.SetInt("HighScore1", 0);
            PlayerPrefs.SetString("HighScoreName1", "AAA");
        }

        if (!PlayerPrefs.HasKey("HighScore2")){
            PlayerPrefs.SetInt("HighScore2", 0);
            PlayerPrefs.SetString("HighScoreName2", "AAA");
        }

        if (!PlayerPrefs.HasKey("HighScore3")){
            PlayerPrefs.SetInt("HighScore3", 0);
            PlayerPrefs.SetString("HighScoreName3", "AAA");
        }

        if (!PlayerPrefs.HasKey("HighScore4")){
            PlayerPrefs.SetInt("HighScore4", 0);
            PlayerPrefs.SetString("HighScoreName4", "AAA");
        }

        if (!PlayerPrefs.HasKey("HighScore5")){
            PlayerPrefs.SetInt("HighScore5", 0);
            PlayerPrefs.SetString("HighScoreName5", "AAA");
        }

        DisplayHighScore();
    }

    public void DisplayHighScore(){
        List<Scores> highScores = new List<Scores>();

        for (int i = 1; i <= 5; i++){
            Scores playerScoreTemp = new Scores(PlayerPrefs.GetInt("HighScore" + i.ToString()), PlayerPrefs.GetString("HighScoreName" + i.ToString()));
            highScores.Add(playerScoreTemp);
        } 

        List<Scores> sortedHighScores = highScores.OrderBy(x => x.playerScore).ToList();
        sortedHighScores.Reverse();


        score1Text.text = sortedHighScores[0].playerScore.ToString();
        score2Text.text = sortedHighScores[1].playerScore.ToString();
        score3Text.text = sortedHighScores[2].playerScore.ToString();
        score4Text.text = sortedHighScores[3].playerScore.ToString();
        score5Text.text = sortedHighScores[4].playerScore.ToString();

        name1Text.text = sortedHighScores[0].playerName;
        name2Text.text = sortedHighScores[1].playerName;
        name3Text.text = sortedHighScores[2].playerName;
        name4Text.text = sortedHighScores[3].playerName;
        name5Text.text = sortedHighScores[4].playerName;

    }

    public void RestartGame(){
        SceneManager.LoadScene("Game");
    }

    public void GoToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetHighScores(){
        PlayerPrefs.DeleteKey("HighScore1");
        PlayerPrefs.DeleteKey("HighScore2");
        PlayerPrefs.DeleteKey("HighScore3");
        PlayerPrefs.DeleteKey("HighScore4");
        PlayerPrefs.DeleteKey("HighScore5");
        PlayerPrefs.DeleteKey("HighScoreName1");
        PlayerPrefs.DeleteKey("HighScoreName2");
        PlayerPrefs.DeleteKey("HighScoreName3");
        PlayerPrefs.DeleteKey("HighScoreName4");
        PlayerPrefs.DeleteKey("HighScoreName5");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class Scores {
    public int playerScore;
    public string  playerName;

    public Scores(int playerScore, string playerName){
        this.playerScore = playerScore;
        this.playerName = playerName;
    }

}
