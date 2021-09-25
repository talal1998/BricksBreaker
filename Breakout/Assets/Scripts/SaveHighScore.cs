using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class SaveHighScore : MonoBehaviour
{
    public void SaveScore(){
        List<Scores1> highScores = new List<Scores1>();

        int playerScore = GameObject.Find("UIManager").GetComponent<UIManager>().score;
        string playerName = GameObject.Find("GameManager").GetComponent<GameManager>().yourNameText.text;

        for (int i = 1; i <= 5; i++){
            Scores1 playerScoreTemp = new Scores1(PlayerPrefs.GetInt("HighScore" + i.ToString()), PlayerPrefs.GetString("HighScoreName" + i.ToString()));
            highScores.Add(playerScoreTemp);
        } 
        
        Scores1 currentScore = new Scores1(playerScore, playerName);
        highScores.Add(currentScore);


        List<Scores1> sortedHighScores = highScores.OrderBy(x => x.playerScore).ToList();

        sortedHighScores.Reverse(); 
        sortedHighScores.RemoveAt(5);


        for (int i = 1; i <= 5; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i.ToString(), sortedHighScores[i - 1].playerScore);
            PlayerPrefs.SetString("HighScoreName" + i.ToString(), sortedHighScores[i - 1].playerName);
        } 


    }

    public void GoToHighScore(){
        SceneManager.LoadScene("HighScoresScene");
    }
}

public class Scores1 {
    public int playerScore;
    public string  playerName;

    public Scores1(int playerScore, string playerName){
        this.playerScore = playerScore;
        this.playerName = playerName;
    }

}
