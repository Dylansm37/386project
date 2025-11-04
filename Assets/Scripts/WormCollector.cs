using UnityEngine;
using TMPro;

public class WormCollector : MonoBehaviour
{
    [Header("Worm Collection")]
    public int wormsCollected = 0;
    public int totalWorms = 10;
    public TMP_Text wormProgressText;
    public GameObject winPanel;

    [Header("Audio")]
    public AudioSource eatSound;

    private GameObject[] allWorms;

    void Start()
    {
        allWorms = GameObject.FindGameObjectsWithTag("Worm");

        // Check if we’re loading a saved game
        if (PlayerPrefs.HasKey("LoadingSavedGame") && PlayerPrefs.GetInt("LoadingSavedGame") == 1)
        {
            LoadProgress();
            PlayerPrefs.DeleteKey("LoadingSavedGame"); // reset flag
        }
        else
        {
            ResetWorms(); // fresh start
        }

        UpdateUI();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Worm"))
        {
            if (eatSound != null)
                eatSound.Play();

            other.gameObject.SetActive(false); // Hide the worm
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

    void WinGame()
    {
        Time.timeScale = 0f;
        if (winPanel != null)
            winPanel.SetActive(true);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("WormsCollected", wormsCollected);

        // Save each worm's collected state
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
        Debug.Log("Saved worm progress: " + wormsCollected);
    }

    public void LoadProgress()
    {
        wormsCollected = PlayerPrefs.GetInt("WormsCollected", 0);
        UpdateUI();

        allWorms = GameObject.FindGameObjectsWithTag("Worm");

        foreach (GameObject worm in allWorms)
        {
            Worm w = worm.GetComponent<Worm>();
            if (w != null)
            {
                int collected = PlayerPrefs.GetInt("WormCollected_" + w.wormID, 0);
                worm.SetActive(collected == 0); // if 0 → visible, if 1 → hidden
            }
        }

        Debug.Log($"Loaded worm progress: {wormsCollected}/{totalWorms}");
    }

    public void ResetWorms()
    {
        allWorms = GameObject.FindGameObjectsWithTag("Worm");
        foreach (GameObject worm in allWorms)
        {
            worm.SetActive(true);
        }
        wormsCollected = 0;
        UpdateUI();
    }
}
