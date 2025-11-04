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
    public AudioSource eatSound;  // Assign an AudioSource with the eat sound in Inspector

    void Start()
    {
        UpdateUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Worm"))
        {
            // Play the eat sound (stops any currently playing instance)
            if (eatSound != null)
            {
                eatSound.Play();
            }

            Destroy(other.gameObject);
            wormsCollected++;
            UpdateUI();

            if (wormsCollected >= totalWorms)
            {
                Time.timeScale = 0f;  // Pause game
                WinGame();
            }
        }
    }

    void UpdateUI()
    {
        if (wormProgressText != null)
            wormProgressText.text = $"Worms: {wormsCollected}/{totalWorms}";
    }

    void WinGame()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}
