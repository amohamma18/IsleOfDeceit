using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    [SerializeField] private Image itemIconImage;

    public event Action<InventorySlotData> OnSlotClicked;

    private InventorySlotData slotData;

    public void SetSlotData(InventorySlotData slotData)
    {
        this.slotData = slotData;
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        if (slotData != null)
        {
            itemNameText.text = slotData.ItemData.ItemName;
            itemAmountText.text = slotData.Amount.ToString();
            //itemIconImage.sprite = slotData.ItemData.Icon;
        }
        else
        {
            itemNameText.text = string.Empty;
            itemAmountText.text = string.Empty;
            itemIconImage.sprite = null;
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        // Visually show that the slot is being hovered over
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Visually show that the slot is no longer being hovered over
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotData != null)
        {
            OnSlotClicked?.Invoke(slotData);
        }
    }
}
