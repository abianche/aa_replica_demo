using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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
        if(Score.PinCount >= GetCurrentLevel().pins)
        {
            NextLevel();
        }
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
