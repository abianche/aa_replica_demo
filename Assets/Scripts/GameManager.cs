using UnityEngine;
using UnityEngine.SceneManagement;

public enum State
{
    Pause, Running
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

    public bool gameHasEnded = false;

    public Rotator rotator;
    public Spawner spawner;
    public Camera mainCamera;

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
        Score.PinCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
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
