using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AudioSource AudioSource;

    private void Start() {
        this.AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}