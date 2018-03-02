using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public void RestartLevel()
    {
        GameManager.instance.RestartLevel();
    }
}
