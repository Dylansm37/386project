using UnityEngine;
using TMPro;

public class WormCollector : MonoBehaviour
{
    [Header("Worm Collection")]
    public int wormsCollected = 0;
    public int totalWorms = 10;

    [Header("UI & Panels")]
    public TextMeshProUGUI wormProgressText;      // Scene TextMeshProUGUI
    public GameObject winPanelPrefab;             // Assign prefab in project (not scene)
    private GameObject winPanelInstance;         // Runtime instance of WinPanel

    [Header("Audio")]
    public AudioSource eatSound;

    private GameObject[] allWorms;

    /// <summary>
    /// Call this AFTER the player avatar spawns to initialize the collector.
    /// </summary>
    public void InitializeCollector()
    {
        // Find all worms in the scene
        allWorms = GameObject.FindGameObjectsWithTag("Worm");
        totalWorms = allWorms.Length;

        // --- Setup WormProgressText ---
        if (wormProgressText == null)
        {
            wormProgressText = GameObject.Find("WormProgressText")?.GetComponent<TextMeshProUGUI>();
            if (wormProgressText == null)
                Debug.LogWarning("No WormProgressText found in the scene!");
        }

        // --- Setup WinPanel ---
        if (winPanelInstance == null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null && winPanelPrefab != null)
            {
                winPanelInstance = Instantiate(winPanelPrefab, canvas.transform);
                winPanelInstance.name = "WinPanel";
                winPanelInstance.SetActive(false);
            }
            else if (canvas == null)
            {
                Debug.LogError("No Canvas found in scene to instantiate WinPanel!");
            }
            else if (winPanelPrefab == null)
            {
                Debug.LogError("WinPanel prefab not assigned! Cannot show win screen.");
            }
        }

        // --- Load or reset worms ---
        bool loadingSaved = PlayerPrefs.GetInt("LoadingSavedGame", 0) == 1;
        if (loadingSaved)
        {
            LoadProgress();
            PlayerPrefs.SetInt("LoadingSavedGame", 0);
        }
        else
        {
            ResetWorms();
        }

        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Worm"))
        {
            if (eatSound != null)
                eatSound.Play();

            other.gameObject.SetActive(false);
            wormsCollected++;

            UpdateUI();

            if (wormsCollected >= totalWorms)
                WinGame();
        }
    }

    public void UpdateUI()
    {
        if (wormProgressText != null)
            wormProgressText.text = $"Worms: {wormsCollected}/{totalWorms}";
    }

    public void WinGame()
    {
        Time.timeScale = 0f;

        if (winPanelInstance != null)
        {
            winPanelInstance.SetActive(true);
            Debug.Log("WinPanel activated! You win!");
        }
        else
        {
            Debug.LogError("WinPanel instance missing! Cannot show win screen.");
        }
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("WormsCollected", wormsCollected);

        foreach (GameObject worm in allWorms)
        {
            if (worm != null)
            {
                Worm w = worm.GetComponent<Worm>();
                if (w != null)
                    PlayerPrefs.SetInt("WormCollected_" + w.wormID, worm.activeSelf ? 0 : 1);
            }
        }

        PlayerPrefs.Save();
        Debug.Log("Worm progress saved: " + wormsCollected);
    }

    public void LoadProgress()
    {
        wormsCollected = PlayerPrefs.GetInt("WormsCollected", 0);

        allWorms = GameObject.FindGameObjectsWithTag("Worm");
        foreach (GameObject worm in allWorms)
        {
            Worm w = worm.GetComponent<Worm>();
            if (w != null)
            {
                int collected = PlayerPrefs.GetInt("WormCollected_" + w.wormID, 0);
                worm.SetActive(collected == 0);
            }
        }

        UpdateUI();
    }

    public void ResetWorms()
    {
        allWorms = GameObject.FindGameObjectsWithTag("Worm");
        foreach (GameObject worm in allWorms)
            worm.SetActive(true);

        wormsCollected = 0;
        UpdateUI();
    }
}
