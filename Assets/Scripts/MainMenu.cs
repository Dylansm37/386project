using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start a new game
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your main game scene
    }

    // Quit game
    public void QuitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Load last saved game
    public void LoadGame()
    {
        string savedScene = PlayerPrefs.GetString("SavedScene", "GameScene"); // fallback

        // Subscribe to sceneLoaded callback
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the saved scene
        SceneManager.LoadScene(savedScene);
    }

    // Called when a new scene is loaded
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

        Debug.Log("Game Loaded from scene: " + scene.name);

        // Unsubscribe from the callback to avoid running it again
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
