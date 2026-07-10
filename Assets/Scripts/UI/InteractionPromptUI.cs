using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private TMP_Text promptText;

    private void Awake()
    {
        HidePrompt();
    }
    public void ShowPrompt(string prompt)
    {
        promptText.text = $"[E] {prompt}";
        promptText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }
}
