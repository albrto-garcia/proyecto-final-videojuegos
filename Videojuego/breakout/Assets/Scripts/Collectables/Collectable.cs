using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private AudioSource AudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        { 
            this.AudioSource = GetComponent<AudioSource>();
            AudioSource.Play();
            this.ApplyEffect();
        }

        if (collision.tag == "Paddle" || collision.tag == "DeathWall")
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void ApplyEffect();
}
