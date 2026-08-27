using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
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
            if (slotData.Amount > 1)
            {
                itemAmountText.text = slotData.Amount.ToString();
            }
            else
            {
                itemAmountText.text = string.Empty;
            }
            itemIconImage.sprite = slotData.ItemData.Icon;
        }
        else
        {
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
