using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Difficulty Settings")]
    public TMP_Dropdown difficultyDropdown;
    public static int DifficultyLevel;

    void Start()
    {
        DifficultyLevel = PlayerPrefs.GetInt("DifficultyLevel", 1);

        if (difficultyDropdown != null)
        {
            difficultyDropdown.value = DifficultyLevel;
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        }
    }

    public void OnDifficultyChanged(int value)
    {
        DifficultyLevel = value;
        PlayerPrefs.SetInt("DifficultyLevel", value);
        PlayerPrefs.Save();
    }

    // -----------------------------
    // NEW GAME
    // -----------------------------
    public void StartNewGame()
    {
        // Clear saved progress
        PlayerPrefs.DeleteKey("WormsCollected");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("TimeElapsed");
        PlayerPrefs.DeleteKey("LoadingSavedGame");

        for (int i = 0; i < 100; i++)
            PlayerPrefs.DeleteKey("WormCollected_" + i);

        PlayerPrefs.Save();

        // Subscribe to sceneLoaded to initialize the player
        SceneManager.sceneLoaded += OnGameSceneLoaded;
        SceneManager.LoadScene("GameScene");
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "GameScene") return;

        // Find the spawned player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            WormCollector collector = player.GetComponent<WormCollector>();
            if (collector != null)
            {
                // Start fresh: Reset worms and UI
                collector.ResetWorms();
                collector.InitializeCollector();
            }
            else
            {
                Debug.LogWarning("No WormCollector found on the spawned player!");
            }
        }
        else
        {
            Debug.LogWarning("Player not found in the scene after loading GameScene!");
        }

        // Unsubscribe to avoid running again
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }

    // -----------------------------
    // LOAD GAME
    // -----------------------------
    public void LoadGame()
    {
        string savedScene = PlayerPrefs.GetString("SavedScene", "GameScene");

        // Mark that we are loading a saved game
        PlayerPrefs.SetInt("LoadingSavedGame", 1);
        PlayerPrefs.Save();

        // Subscribe to sceneLoaded
        SceneManager.sceneLoaded += OnLoadScene;
        SceneManager.LoadScene(savedScene);
    }

    private void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "GameScene") return;

        // Find the spawned player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // Restore player position
            float x = PlayerPrefs.GetFloat("PlayerX", player.transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerY", player.transform.position.y);
            float z = PlayerPrefs.GetFloat("PlayerZ", player.transform.position.z);
            player.transform.position = new Vector3(x, y, z);

            // Initialize WormCollector and load saved worm progress
            WormCollector collector = player.GetComponent<WormCollector>();
            if (collector != null)
            {
                collector.InitializeCollector();
            }
            else
            {
                Debug.LogWarning("No WormCollector found on the spawned player!");
            }
        }
        else
        {
            Debug.LogWarning("Player not found in the scene after loading saved game!");
        }

        // Unsubscribe
        SceneManager.sceneLoaded -= OnLoadScene;
    }

    // -----------------------------
    // QUIT GAME
    // -----------------------------
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
