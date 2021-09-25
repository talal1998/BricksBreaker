using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreText;
    public Text targetText;
    public Text livesText;
    public Text levelsText;

    public int score { get; set; }


    private void Awake(){
        Brick.OnBrickDestruction += OnBrickDestruction;
        BricksManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLiveLost += OnLiveLost;
    }

    public void Start(){
        OnLiveLost(GameManager.Instance.availableLives);
        setCurrentLevelText();
    }

    private void OnBrickDestruction(Brick obj){
        UpdateRemainingBricksText();
        UpdateScoreText(10);
    }

    private void UpdateRemainingBricksText(){
        targetText.text = $"TARGET:{Environment.NewLine}{BricksManager.Instance.RemainingBricks.Count} / {BricksManager.Instance.InitialBricksCount}";
    }

    private void OnLevelLoaded(){
        UpdateRemainingBricksText();
        UpdateScoreText(0);
    }
    
    public void DisplayHighScore(){

    }

    public void setCurrentLevelText(){
        int currentLevel = BricksManager.Instance.CurrentLevel;
        levelsText.text = $"LEVEL: {currentLevel + 1}";
    }


    private void OnLiveLost(int remainingLives){
        livesText.text = $"LIVES:{Environment.NewLine}{remainingLives}/3";
    }

    public void UpdateScoreText(int increment){
        this.score += increment;
        string scoreString = this.score.ToString().PadLeft(5, '0');
        scoreText.text = $"SCORE:{Environment.NewLine}{scoreString}"; 
    }

    public void ResetScoreText(){
        this.score = 0;
        string scoreString = this.score.ToString().PadLeft(5, '0');
        scoreText.text = $"SCORE:{Environment.NewLine}{scoreString}"; 
    }

    private void OnDisable(){
        Brick.OnBrickDestruction -= OnBrickDestruction;
        BricksManager.OnLevelLoaded -= OnLevelLoaded;
        GameManager.OnLiveLost -= OnLiveLost;
    }

    
}
