using UnityEngine;
using TMPro;

public class WormCollector : MonoBehaviour
{
    public int wormsCollected = 0;
    public int totalWorms = 10;
    public TMP_Text wormProgressText;
    public GameObject winPanel; // ← Link this in Inspector!

    void Start()
    {
        UpdateUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Worm"))
        {
            Destroy(other.gameObject);
            wormsCollected++;
            UpdateUI();

            if (wormsCollected >= totalWorms)
            {
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
        Debug.Log("You collected all the worms! You win!");

        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f; // Optional: pause game
    }
}
