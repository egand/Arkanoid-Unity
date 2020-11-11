using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;
    public ParticleSystem destroyEffect;

    private SpriteRenderer _sr;

    private void Start()
    {
        this._sr = this.GetComponent<SpriteRenderer>();
        this._sr.sprite = BricksManager.Instance.sprites[this.hitPoints - 1];
    }

    //public static event Action<Brick> onBrickDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.hitPoints--;

        if (this.hitPoints <= 0)
        {
            //onBrickDestruction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            // change the sprite
            this._sr.sprite = BricksManager.Instance.sprites[this.hitPoints - 1];
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector2 brickPos = gameObject.transform.position;
        Vector3 spawnEffectPos = new Vector3(brickPos.x, brickPos.y, -0.2f);
        GameObject effect = Instantiate(destroyEffect.gameObject, spawnEffectPos, Quaternion.identity);
        ParticleSystem.MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this._sr.color;
        Destroy(effect, destroyEffect.main.startLifetime.constant);

    }
}
