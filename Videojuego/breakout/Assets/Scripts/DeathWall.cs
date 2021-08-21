using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private AudioSource AudioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            this.AudioSource = GetComponent<AudioSource>();
            AudioSource.Play();
            Ball ball = collision.GetComponent<Ball>();
            BallsManager.Instance.Balls.Remove(ball);
            ball.Die();
        }
    } 
}
