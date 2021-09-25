using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region  Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    void Awake()
    {
        if (_instance != null) 
        {
            Destroy(gameObject);
        } 
        else 
        {
            _instance = this;
        }
        
    }

    #endregion
    
    public Text scoreText;
    public Text highScoreText;
    public Text yourNameText;
    
    public GameObject scoreToggle;
    public GameObject victoryScreen;
    public GameObject gameOverScreen;
    public bool isGameStarted { get; set; }
    public bool isVictoryScreenActive { get; set; }
    public int lives { get; set; }
    public int availableLives = 3;
    public static event Action<int> OnLiveLost;

    private void Start(){
        this.lives = this.availableLives;
        Screen.SetResolution(540, 960, false);
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestruction += OnBrickDestruction;
    }

    public void ShowVictoryScreen(){
        this.isVictoryScreenActive = true;
        scoreToggle.SetActive(true);
        victoryScreen.SetActive(true);
        int score = GameObject.Find("UIManager").GetComponent<UIManager>().score;
        int highScore = GetHighestScore();
        scoreText.text = $"Your Score: {score}";
        Debug.Log(score);
        
        if (score > highScore){
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = $"High Score: {score}";
            yourNameText.gameObject.SetActive(true);
        } else {
            highScoreText.text = $"High Score: {highScore}";
        }
    }

    public void ShowHighScoreScreen(){
        SceneManager.LoadScene("HighScoresScene");
    }

    private void OnBrickDestruction(Brick obj){
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            BallsManager.Instance.ResetBalls();
            GameManager.Instance.isGameStarted = false;
            BricksManager.Instance.LoadNextLevel();
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnBallDeath(Ball obj){
        if (BallsManager.Instance.Balls.Count <= 0){
            this.lives--;
            //GameObject.Find("UIManager").GetComponent<UIManager>().ResetScoreText();

            if (this.lives < 1) {
                GameOver();
            } else {
                OnLiveLost?.Invoke(this.lives);
                isGameStarted = false;
                //BricksManager.Instance.LoadLevel(BricksManager.Instance.CurrentLevel);            
                BallsManager.Instance.ResetBalls();
            }
        }
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public int GetHighestScore(){
        int highestScore = 0;

        for (int i = 1; i <= 5; i++){
            int playerScoreTemp = PlayerPrefs.GetInt("HighScore" + i.ToString());
            if (playerScoreTemp > highestScore){
                highestScore = playerScoreTemp;
            }
        }
        return highestScore;
    }

    void GameOver(){
        scoreToggle.SetActive(true);
        gameOverScreen.SetActive(true);
        int score = GameObject.Find("UIManager").GetComponent<UIManager>().score;
        int highScore = GetHighestScore();
        scoreText.text = $"Your Score: {score}";
        
        if (score > highScore){
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = $"High Score: {score}";
            yourNameText.gameObject.SetActive(true);
        } else {
            highScoreText.text = $"High Score: {highScore}";
        }
    }

    private void OnDisable(){
        Ball.OnBallDeath -= OnBallDeath;
    }

}

