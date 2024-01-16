using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;



public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, deselectedColor;
    //Call this function every time you open the inventory menu

    private void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;    
    }
    public void Deselect()
    {
        image.color = deselectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0) //checks if slot is empty
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem item = dropped.GetComponent<InventoryItem>();
            item.parentAfterDrag = transform;
        }
    }
}
