using UnityEngine;
using TMPro;

public class WormCollector : MonoBehaviour
{
    public int wormsCollected = 0;
    public int totalWorms = 10;
    public TMP_Text wormProgressText;
    public GameObject winPanel; 

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
                Time.timeScale = 0f;
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
