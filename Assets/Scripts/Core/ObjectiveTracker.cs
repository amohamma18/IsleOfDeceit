using System;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    [SerializeField] private InventoryCoordinator inventoryCoordinator;
    [SerializeField] private ItemData targetItem;
    [SerializeField] private int targetAmount;

    private bool isComplete = false;

    public event Action ObjectiveCompleted;

    public event Action<int, int> ProgressChanged;

    public ItemData TargetItem => targetItem;

    private void Start()
    {
        CheckProgress();
    }

    private void OnEnable()
    {
        inventoryCoordinator.OnInventoryChanged += CheckProgress;
    }

    private void OnDisable()
    {
        inventoryCoordinator.OnInventoryChanged -= CheckProgress;
    }
    private void CheckProgress()
    {
        if (isComplete) { return; }

        int currentAmount = inventoryCoordinator.GetItemAmount(targetItem);

        ProgressChanged?.Invoke(currentAmount, targetAmount);

        if (currentAmount < targetAmount) { return; }

        isComplete = true;
        ObjectiveCompleted?.Invoke();
    }
}
