using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip paddle, rebound;

    private AudioSource AudioSource;

    private SpriteRenderer sr;

    public bool isLightningBall;

    public ParticleSystem lightningBallEffect;

    public float lightningBallDuration = 10;

    public static event Action<Ball> OnBallDeath;
    public static event Action<Ball> OnLightningBallEnable;
    public static event Action<Ball> OnLightningBallDisable;

    private void Awake()
    {
        this.sr = GetComponentInChildren<SpriteRenderer>();
        this.AudioSource = GetComponent<AudioSource>();
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }

    public void StartLightningBall()
    {
        if (!this.isLightningBall)
        {
            this.isLightningBall = true;
            this.sr.enabled = false;
            lightningBallEffect.gameObject.SetActive(true);
            StartCoroutine(StopLightningBallAfterTime(this.lightningBallDuration));

            OnLightningBallEnable?.Invoke(this);
        }
    }

    private IEnumerator StopLightningBallAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StopLightningBall();
    }

    private void StopLightningBall()
    {
        if (this.isLightningBall)
        {
            this.isLightningBall = false;
            this.sr.enabled = true;
            lightningBallEffect.gameObject.SetActive(false);

            OnLightningBallDisable?.Invoke(this);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Paddle")
            AudioSource.clip = paddle;
        else
            if(collision.gameObject.name == "LeftWall" ||
               collision.gameObject.name == "RightWall" || 
               collision.gameObject.name == "TopWall")
                AudioSource.clip = rebound;
        
        AudioSource.Play();
    }
}
