using UnityEngine;
using UnityEngine.UI;

public class AvatarSelectionUI : MonoBehaviour
{
    [Header("Avatar Buttons")]
    public Button[] avatarButtons;          // Assign in Inspector
    public Image[] highlightBorders;        // Optional: highlight the selected one

    private int selectedAvatarIndex = 0;

    void Start()
    {
        selectedAvatarIndex = PlayerPrefs.GetInt("SelectedAvatar", 0);
        UpdateHighlights();

        // Hook up all button listeners
        for (int i = 0; i < avatarButtons.Length; i++)
        {
            int index = i; // local copy for lambda capture
            avatarButtons[i].onClick.AddListener(() => SelectAvatar(index));
        }
    }

    public void SelectAvatar(int index)
    {
        selectedAvatarIndex = index;
        PlayerPrefs.SetInt("SelectedAvatar", index);
        PlayerPrefs.Save();

        UpdateHighlights();

        Debug.Log("Selected avatar: " + index);
    }

    private void UpdateHighlights()
    {
        // Optional: visually show which avatar is selected
        for (int i = 0; i < highlightBorders.Length; i++)
        {
            highlightBorders[i].enabled = (i == selectedAvatarIndex);
        }
    }
}
