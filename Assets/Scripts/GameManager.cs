using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum State
{
    Pause, Running
}

[System.Serializable]
public class Level
{
    public string objective;
    public int pins;
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private State state;
    public void SetState(State state)
    {
        this.state = state;
    }
    public bool isPaused()
    {
        return this.state == State.Pause;
    }

    public AudioSource audioSource;

    [SerializeField]
    Transform PausedPanel; //Will assign our panel to this variable so we can enable/disable it

    public Level[] levels;
    private int currentLevel = 0;

    public bool gameHasEnded = false;

    public Rotator rotator;
    public Spawner spawner;
    public Camera mainCamera;

    public Text objective;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // DontDestroyOnLoad(gameObject);
        SetState(State.Running);
        mainCamera = Camera.main;
    }

    private void Start()
    {
        if (levels.Length == 0)
        {
            Debug.LogError("GM: No levels to play!");
            return;
        }

        NextLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (Score.PinCount >= GetCurrentLevel().pins)
        {
            NextLevel();
        }
    }

    public void TogglePause()
    {
        if (isPaused())
        {
            SetState(State.Running);
            PausedPanel.gameObject.SetActive(false); //turn off pause menu
            Time.timeScale = 1f; //resume game
            return;
        }

        SetState(State.Pause);
        PausedPanel.gameObject.SetActive(true); //turn on the pause menu
        Time.timeScale = 0f; //pause the game
    }

    public void Resume()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private Level GetCurrentLevel()
    {
        return levels[currentLevel - 1];
    }

    public void NextLevel()
    {
        currentLevel++;
        if (levels.Length < currentLevel)
        {
            // no more levels
            EndGame();
            return;
        }

        // Score.PinCount = 0;
        objective.text = levels[currentLevel - 1].objective;
    }

    public void EndGame()
    {
        if (gameHasEnded)
            return;

        rotator.enabled = false;
        spawner.enabled = false;

        mainCamera.GetComponent<Animator>().SetTrigger("EndGame");
        gameHasEnded = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
