using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject mainMenuPanel; 

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
