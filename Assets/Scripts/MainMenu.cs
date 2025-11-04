using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        // Clear saved progress
        PlayerPrefs.DeleteKey("WormsCollected");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("TimeElapsed");


        // 🪱 Clear individual worm save data
        for (int i = 0; i < 100; i++) // adjust if you have more than 100 worms
        {
            PlayerPrefs.DeleteKey("WormCollected_" + i);
        }

        PlayerPrefs.Save();

        // Load the game scene fresh
        SceneManager.LoadScene("GameScene");
    }


    public void LoadGame()
    {
        string savedScene = PlayerPrefs.GetString("SavedScene", "GameScene"); // fallback

        // Tell WormCollector we’re loading saved data
        PlayerPrefs.SetInt("LoadingSavedGame", 1);
        PlayerPrefs.Save();

        // Subscribe to sceneLoaded callback
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the saved scene
        SceneManager.LoadScene(savedScene);
    }


    public void QuitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find player in the loaded scene
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerX", player.transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerY", player.transform.position.y);
            float z = PlayerPrefs.GetFloat("PlayerZ", player.transform.position.z);

            player.transform.position = new Vector3(x, y, z);
        }

        // Restore volume
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = volume;

        // 🪱 Restore worm progress
        WormCollector wormCollector = FindObjectOfType<WormCollector>();
        if (wormCollector != null)
        {
            wormCollector.LoadProgress();
            Debug.Log("Worm progress loaded: " + wormCollector.wormsCollected);
        }
        else
        {
            Debug.LogWarning("No WormCollector found in the loaded scene.");
        }

        GameTimer timer = FindObjectOfType<GameTimer>();
        if (timer != null)
            timer.LoadTime();

        Debug.Log("Game Loaded from scene: " + scene.name);

        // Unsubscribe from the callback to avoid running it again
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
