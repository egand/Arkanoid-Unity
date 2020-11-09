using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Camera mainCamera;
    private float paddleInitialY;
    private SpriteRenderer sr;
    private float borderSx = 30;
    private float borderDx = 510;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        paddleInitialY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        float paddleWidth = sr.size.x * sr.sprite.pixelsPerUnit;
        float leftClamp = borderSx + (paddleWidth / 2);
        float rightClamp = borderDx - (paddleWidth / 2);
        float mousePosPixel = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePosX = mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixel, 0, 0)).x;
        this.transform.position = new Vector3(mousePosX, paddleInitialY, 0);
    }
}
