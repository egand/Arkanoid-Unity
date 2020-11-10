using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    #region Singleton
    private static Paddle _instance;

    public static Paddle Instance => _instance;

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

    private Camera _mainCamera;
    private float _paddleInitialY;
    private SpriteRenderer _sr;
    private float _borderSx;
    private float _borderDx;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _sr = GetComponent<SpriteRenderer>();
        _paddleInitialY = this.transform.position.y;
        _borderSx = 30;
        _borderDx = 510;
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        float paddleWidth = _sr.size.x * _sr.sprite.pixelsPerUnit;
        float leftClamp = _borderSx + (paddleWidth / 2);
        float rightClamp = _borderDx - (paddleWidth / 2);
        float mousePosPixel = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePosX = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixel, 0, 0)).x;
        this.transform.position = new Vector2(mousePosX, _paddleInitialY);
    }
}
