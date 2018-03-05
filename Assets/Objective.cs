using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level
{
    public string objective;
    public int pins;
    private bool completed = false;

    public bool isCompleted()
    {
        return completed;
    }

    public void Complete()
    {
        completed = true;
    }
}

public class Objective : MonoBehaviour
{
    public Level[] levels;

    private Text objectiveText;
    private int currentLevel;

    private void Awake()
    {
        objectiveText = GetComponent<Text>();
        currentLevel = 0;
    }

    void Start()
    {
        if (levels.Length == 0)
        {
            Debug.LogError("Objective: No levels to play!");
            return;
        }

        objectiveText.text = levels[0].objective;
    }

    void Update()
    {
        if (Score.PinCount >= GetCurrentLevel().pins)
        {
            NextLevel();
        }
    }

    private Level GetCurrentLevel()
    {
        return levels[currentLevel];
    }

    public void NextLevel()
    {
        currentLevel++;
        if (levels.Length <= currentLevel)
        {
            // no more levels
            GameManager.instance.RestartLevel();
            return;
        }

        // Score.PinCount = 0;
        objectiveText.text = levels[currentLevel].objective;
    }
}
