using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject mainMenuPanel; // Optional, if you're hiding/showing panels

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
    }

    public void HideControls()
    {
        controlsPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }
}
