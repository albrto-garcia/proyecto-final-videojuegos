using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
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

    public AudioClip Intro, Final, Button, Victory;
    private AudioSource AudioSource;
    public GameObject gameOverScreen;

    public GameObject victoryScreen;

    public GameObject menuScreen;

    public int AvailibleLives = 3;

    public int Lives { get; set; }

    public bool IsGameStarted { get; set; }

    public static event Action<int> OnLiveLost;

    private void Start()
    {
        this.Lives = this.AvailibleLives;
        //Screen.SetResolution(540, 960, false);
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestruction += OnBrickDestruction;
        AudioSource = GetComponent<AudioSource>();
        AudioSource.loop = false;
        AudioSource.clip = Button;
        AudioSource.Play();
        gameOverScreen.SetActive(false); 
        victoryScreen.SetActive(false); 
        menuScreen.SetActive(true); 
        AudioSource.loop = true;
        AudioSource.clip = Intro;
        AudioSource.Play();
    }

    private void OnBrickDestruction(Brick obj)
    {
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            BallsManager.Instance.ResetBalls();
            GameManager.Instance.IsGameStarted = false;
            BricksManager.Instance.LoadNextLevel();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        AudioSource.loop = false;
        AudioSource.clip = Button;
        AudioSource.Play();
        gameOverScreen.SetActive(false); 
        victoryScreen.SetActive(false); 
        menuScreen.SetActive(false); 
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            if (this.Lives < 1)
            {
                menuScreen.SetActive(false); 
                victoryScreen.SetActive(false);
                gameOverScreen.SetActive(true);
                AudioSource.loop = false;
                AudioSource.clip = Final;
                AudioSource.Play();
            }
            else
            {
                OnLiveLost?.Invoke(this.Lives);
                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
                BricksManager.Instance.LoadLevel(BricksManager.Instance.CurrentLevel);
            }
        }
    }

    internal void ShowVictoryScreen()
    {
        gameOverScreen.SetActive(false); 
        menuScreen.SetActive(false); 
        victoryScreen.SetActive(true);
        AudioSource.loop = false;
        AudioSource.clip = Victory;
        AudioSource.Play();
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }
}
