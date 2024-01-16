using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange;
    public Item item;

    public static string[] interactableItems = new string[] {
        "Stone"
    };
    public static string[] nonInteractableItems = new string[] {
        "Tree"
    };
    public string GetItemName()
    {
        return ItemName;
    }

    //if an agent enters the proximity check that it is the player
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    //if an agent exits the proximity check that it is the player
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        // adds item in the inventory only if, mouse clicked, we are in range, item is on target, and the object we want right now is the selected object. 
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject) {
            if (interactableItems.Contains(ItemName))
            {
                // if inventory is not full, then add to inventory
                if (!InventorySystem.Instance.isFull())
                {
                    InventorySystem.Instance.AddItemToInventory(item);
                    Debug.Log("item addded to inventory");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory is Full");
                }
            }
        }
    }
}
