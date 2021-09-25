using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{

    #region  Singleton

    private static BallsManager _instance;

    public static BallsManager Instance => _instance;

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

    [SerializeField]
    private Ball ballPrefab;
    private Ball initialBall; 
    private Rigidbody2D initialBallRb;
    public int ballsOnScreen;

    public float initialBallSpeed = 250;

    public int maxBallCount = 10;

    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            //align ball position to the paddle position
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
            initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                initialBallRb.isKinematic = false;
                GameManager.Instance.isGameStarted = true;
                initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
            }
        }
    }

    public void SpawnBalls(Vector3 position){
        ballsOnScreen = GameObject.FindGameObjectsWithTag("Ball").Length;

        if (ballsOnScreen < maxBallCount){
            Ball spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as Ball;
            Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
            spawnedBallRb.isKinematic = false;
            spawnedBallRb.AddForce(new Vector2(0, initialBallSpeed));
            this.Balls.Add(spawnedBall);
        }
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .27f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            initialBall
        };
    }

    public void ResetBalls(){
        foreach (var ball in this.Balls.ToList()){
            Destroy(ball.gameObject);
        }
        InitBall();
    }
}
