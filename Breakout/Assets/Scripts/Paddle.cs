using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region  Singleton

    private static Paddle _instance;

    public static Paddle Instance => _instance;

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

    private Camera mainCamera;
    private float paddleInitialY;
    private float defaultPaddleWidthInPixels = 200; 
    private float defaultLeftCamp = 135;
    private float defaultRightClamp = 410;
    private SpriteRenderer sr;
    private BoxCollider2D boxCol;
    public bool PaddleIsTransforming { get; set; }


    public float extrendShrinkDuration = 10;
    public float paddleWidth = 2;
    public float paddleHeight = 0.20f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleInitialY = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void PaddleMovemement()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isVictoryScreenActive == false){
            float paddleShift = (defaultPaddleWidthInPixels - ((defaultPaddleWidthInPixels / 2) * this.sr.size.x)) / 2;
            float leftClamp = defaultLeftCamp - paddleShift;
            float rightClamp = defaultRightClamp + paddleShift;
            float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
            float mousePositionWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
            this.transform.position = new Vector3(mousePositionWorldX, paddleInitialY, 0);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        PaddleMovemement();        
    }

    public void StartWidthAnimation(float newWidth){
        StartCoroutine(AnimatePaddleWidth(newWidth));
    }

    public IEnumerator AnimatePaddleWidth(float width){
        this.PaddleIsTransforming = true;
        this.StartCoroutine(ResetPaddleWidthAfterTime(this.extrendShrinkDuration));

        if (width > this.sr.size.x){
            float currentWidth = this.sr.size.x;
            while (currentWidth < width){
                currentWidth += Time.deltaTime * 2;
                this.sr.size = new Vector2(currentWidth, paddleHeight);
                boxCol.size = new Vector2(currentWidth, paddleHeight);
                yield return null;
            }
        } else {
            float currentWidth = this.sr.size.x;
            while (currentWidth > width){
                currentWidth -= Time.deltaTime * 2;
                this.sr.size = new Vector2(currentWidth, paddleHeight);
                boxCol.size = new Vector2(currentWidth, paddleHeight);
                yield return null;
            }
        }
        this.PaddleIsTransforming = false;

    }

    private IEnumerator ResetPaddleWidthAfterTime(float seconds){
        yield return new WaitForSeconds(seconds);
        this.StartWidthAnimation(this.paddleWidth);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            } 
            else 
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
            
        }
    }
}
