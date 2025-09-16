using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // In case the game is paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
