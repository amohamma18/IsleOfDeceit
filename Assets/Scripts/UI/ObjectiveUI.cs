using UnityEngine;
using TMPro;
public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private ObjectiveTracker objectiveTracker;
    [SerializeField] private TextMeshProUGUI progressText;

    [SerializeField] private GameObject completionMessage;

    private void Awake()
    {
        completionMessage.SetActive(false);
    }

    private void OnEnable()
    {
        objectiveTracker.ProgressChanged += UpdateProgressUI;
        objectiveTracker.ObjectiveCompleted += ShowCompletionMessage;
    }

    private void OnDisable()
    {
        objectiveTracker.ProgressChanged -= UpdateProgressUI;
        objectiveTracker.ObjectiveCompleted -= ShowCompletionMessage;
    }

    private void UpdateProgressUI(int currentAmount, int targetAmount)
    {
        int displayAmount = Mathf.Min(currentAmount, targetAmount);
        progressText.text = $"{objectiveTracker.TargetItem.name}: {displayAmount}/{targetAmount}";
    }

    private void ShowCompletionMessage()
    {
        completionMessage.SetActive(true);
    }
}
