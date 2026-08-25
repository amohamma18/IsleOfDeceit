using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryCoordinator inventoryCoordinator;

    private List<InventorySlotUI> toolSlots = new List<InventorySlotUI>();
    private List<InventorySlotUI> resourceSlots = new List<InventorySlotUI>();
    private List<InventorySlotUI> storySlots = new List<InventorySlotUI>();

    private InventorySlotData selectedSlotData;


    [SerializeField] private Transform toolTab;
    [SerializeField] private Transform resourceTab;
    [SerializeField] private Transform storyTab;

    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI actionButtonText;

    private void Awake()
    {
        if (inventoryCoordinator == null)
        {
            Debug.LogError("InventoryCoordinator reference is missing in InventoryUI."); 
            enabled = false;
            return;
        }
        if (toolTab == null || resourceTab == null || storyTab == null)
        {
            Debug.LogError("One or more tab references are missing in InventoryUI."); 
            enabled = false;
            return;
        }
        if (actionButton == null || actionButtonText == null)
        {
            Debug.LogError("One or more action button references are missing in InventoryUI."); 
            enabled = false;
            return;
        }
        toolSlots.AddRange(toolTab.GetComponentsInChildren<InventorySlotUI>());
        resourceSlots.AddRange(resourceTab.GetComponentsInChildren<InventorySlotUI>());
        storySlots.AddRange(storyTab.GetComponentsInChildren<InventorySlotUI>());

        SubscribeToSlots(toolSlots);
        SubscribeToSlots(resourceSlots);
        SubscribeToSlots(storySlots);
    }

    private void OnDestroy()
    {
        UnsubscribeFromSlots(toolSlots);
        UnsubscribeFromSlots(resourceSlots);
        UnsubscribeFromSlots(storySlots);
    }

    private void SubscribeToSlots(List<InventorySlotUI> slots)
    {
        foreach (var slot in slots)
        {
            slot.OnSlotClicked += HandleSlotClicked;
        }
    }

    private void UnsubscribeFromSlots(List<InventorySlotUI> slots)
    {
        foreach (var slot in slots)
        {
            slot.OnSlotClicked -= HandleSlotClicked;
        }
    }

    private void HandleSlotClicked(InventorySlotData slotData)
    {
        selectedSlotData = slotData;
        actionButtonText.text = slotData.ItemData.Action.ToString();
        actionButton.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        inventoryCoordinator.OnInventoryChanged += RefreshUI;
        actionButton.onClick.AddListener(HandleActionButtonClicked);
        RefreshUI();
    }

    private void HandleActionButtonClicked()
    {
        if (selectedSlotData != null) {  return; }

        if (!inventoryCoordinator.PerformItemAction(selectedSlotData.ItemData)) {
            Debug.LogWarning($"Action '{selectedSlotData.ItemData.Action}' could not be performed for item '{selectedSlotData.ItemData.ItemName}'.");
        }
    }

    private void OnDisable()
    {
        inventoryCoordinator.OnInventoryChanged -= RefreshUI;
        actionButton.onClick.RemoveListener(HandleActionButtonClicked);
    }

    private void RefreshUI()
    {
        selectedSlotData = null;
        actionButton.gameObject.SetActive(false);
        List<InventorySlotData> toolItems = new List<InventorySlotData>();
        List<InventorySlotData> resourceItems = new List<InventorySlotData>();
        List<InventorySlotData> storyItems = new List<InventorySlotData>();

        foreach (var item in inventoryCoordinator.GetToolItems())
        {
            toolItems.Add(new InventorySlotData(item, 1));
        }
        foreach (var item in inventoryCoordinator.GetResourceItems())
        {
            resourceItems.Add(new InventorySlotData(item.Key, item.Value));
        }
        foreach (var item in inventoryCoordinator.GetStoryItems())
        {
            storyItems.Add(new InventorySlotData(item.Key, item.Value));
        }

        ClearSlots(toolSlots);
        ClearSlots(resourceSlots);
        ClearSlots(storySlots);

        SetSlots(toolSlots, toolItems);
        SetSlots(resourceSlots, resourceItems);
        SetSlots(storySlots, storyItems);
    }

    private void ClearSlots(List<InventorySlotUI> slots)
    {
        foreach (InventorySlotUI slot in slots)
        {
            slot.SetSlotData(null);
        }
    }

    private void SetSlots(List<InventorySlotUI> slots, List<InventorySlotData> data) {
        for (int i = 0; i < slots.Count && i < data.Count; i++)
        {
            slots[i].SetSlotData(data[i]);
        }
    }
}
