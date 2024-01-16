using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [HideInInspector] public Item item;
    [HideInInspector] public int quantity = 1;
    [HideInInspector] public Transform parentAfterDrag;


    [Header("UI")]
    public Image image;
    public TextMeshProUGUI quantityText;
    
 

    public void RefreshCount()
    {
        quantityText.text = quantity.ToString();
        quantityText.gameObject.SetActive(quantity > 1);
    }
    private void Awake()
    {
        
      
    }
    public void InitialiseItem(Item newItem)
    {
        item = newItem;

        Debug.Log(newItem.image);
        image.sprite = newItem.image;
        RefreshCount();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        Debug.Log("OnEndDrag");
        image.raycastTarget = true;
    }
}