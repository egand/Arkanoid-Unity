using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton
    private static BallsManager _instance;

    public static BallsManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public float initialBallSpeed = 250;
    
    [SerializeField]
    private Ball _ballPrefab;
    public List<Ball> Balls { get; set; }
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;

    private void Start()
    {
        this.Balls = new List<Ball>
        {
            _initialBall
        };
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            _initialBall.transform.position = InitBallPosition();

            if (Input.GetMouseButtonDown(0))
            {
                _initialBallRb.isKinematic = false;
                _initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    private void InitBall()
    {
        _initialBall = Instantiate(_ballPrefab, InitBallPosition(), Quaternion.identity);
        _initialBallRb = _initialBall.GetComponent<Rigidbody2D>();
    }

    private Vector2 InitBallPosition()
    {
        Vector2 paddlePos = Paddle.Instance.gameObject.transform.position;
        float paddleSizeY = Paddle.Instance.GetComponent<SpriteRenderer>().size.y;
        return new Vector2(paddlePos.x, paddlePos.y + paddleSizeY);
    }
}
