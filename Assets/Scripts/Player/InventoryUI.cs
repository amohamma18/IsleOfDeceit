using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum InventoryTab
{
    Tools,
    Resources,
    Story
}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryCoordinator inventoryCoordinator;



    private List<InventorySlotUI> toolSlots = new List<InventorySlotUI>();
    private List<InventorySlotUI> resourceSlots = new List<InventorySlotUI>();
    private List<InventorySlotUI> storySlots = new List<InventorySlotUI>();

    private InventorySlotData selectedSlotData;


    [SerializeField] private RectTransform toolTab;
    [SerializeField] private RectTransform resourceTab;
    [SerializeField] private RectTransform storyTab;

    [SerializeField] private RectTransform itemDetails;

    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI actionButtonText;

    [SerializeField] private Button toolTabButton;

    [SerializeField] private Button resourceTabButton;

    [SerializeField] private Button storyTabButton;

    [SerializeField] private TextMeshProUGUI selectedItemName;

    [SerializeField] private TextMeshProUGUI selectedItemAmountText;

    [SerializeField] private Image selectedItemIcon;

    private UnityAction _switchToToolTab;
    private UnityAction _switchToResourceTab;
    private UnityAction _switchToStoryTab;

    public bool IsInventoryOpen => gameObject.activeSelf;

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
        if (toolTabButton == null || resourceTabButton == null || storyTabButton == null)
        {
            Debug.LogError("One or more tab button references are missing in InventoryUI.");
            enabled = false;
            return;
        }
        if (itemDetails == null || selectedItemName == null || selectedItemAmountText == null || selectedItemIcon == null) 
        {
            Debug.LogError("One or more selected item references are missing in InventoryUI.");
            enabled = false;
            return;
        }


        toolSlots.AddRange(toolTab.GetComponentsInChildren<InventorySlotUI>());
        resourceSlots.AddRange(resourceTab.GetComponentsInChildren<InventorySlotUI>());
        storySlots.AddRange(storyTab.GetComponentsInChildren<InventorySlotUI>());

        _switchToToolTab = () => SwitchTab(InventoryTab.Tools);
        _switchToResourceTab = () => SwitchTab(InventoryTab.Resources);
        _switchToStoryTab = () => SwitchTab(InventoryTab.Story);

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

    private void OnEnable()
    {
        inventoryCoordinator.OnInventoryChanged += RefreshUI;
        actionButton.onClick.AddListener(HandleActionButtonClicked);

        toolTabButton.onClick.AddListener(_switchToToolTab);
        resourceTabButton.onClick.AddListener(_switchToResourceTab);
        storyTabButton.onClick.AddListener(_switchToStoryTab);
        RefreshUI();
    }

    private void OnDisable()
    {
        inventoryCoordinator.OnInventoryChanged -= RefreshUI;
        actionButton.onClick.RemoveListener(HandleActionButtonClicked);
        toolTabButton.onClick.RemoveListener(_switchToToolTab);
        resourceTabButton.onClick.RemoveListener(_switchToResourceTab);
        storyTabButton.onClick.RemoveListener(_switchToStoryTab);
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
        selectedItemName.text = slotData.ItemData.ItemName;
        if (slotData.Amount > 1)
        {
            selectedItemAmountText.text = slotData.Amount.ToString();
        }
        else
        {
            selectedItemAmountText.text = string.Empty;
        }
        selectedItemIcon.sprite = slotData.ItemData.Icon;
        actionButtonText.text = slotData.ItemData.Action.ToString();
        itemDetails.gameObject.SetActive(true);
    }

    private void HandleActionButtonClicked()
    {
        if (selectedSlotData == null) { return; }

        if (!inventoryCoordinator.PerformItemAction(selectedSlotData.ItemData))
        {
            Debug.LogWarning($"Action '{selectedSlotData.ItemData.Action}' could not be performed for item '{selectedSlotData.ItemData.ItemName}'.");
        }
    }

    private void DisableItemDetails()
    {
        selectedSlotData = null;
        itemDetails.gameObject.SetActive(false);
    }


    public void OpenInventory()
    {

        gameObject.SetActive(true);
        SwitchTab(InventoryTab.Tools);

    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }





    private void SwitchTab(InventoryTab tab)
    {
        DisableItemDetails();
        switch (tab) {
            case InventoryTab.Tools:
                toolTab.gameObject.SetActive(true);
                resourceTab.gameObject.SetActive(false);
                storyTab.gameObject.SetActive(false);
                break;
            case InventoryTab.Resources:
                toolTab.gameObject.SetActive(false);
                resourceTab.gameObject.SetActive(true);
                storyTab.gameObject.SetActive(false);
                break;
            case InventoryTab.Story:
                toolTab.gameObject.SetActive(false);
                resourceTab.gameObject.SetActive(false);
                storyTab.gameObject.SetActive(true);
                break;
        }
    }


    private void RefreshUI()
    {
        DisableItemDetails();
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
