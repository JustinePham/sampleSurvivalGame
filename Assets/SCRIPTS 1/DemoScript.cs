using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     This is just a script of test functions for inventory functionality.
/// </summary>
public class DemoScript : MonoBehaviour
{
    public InventorySystem inventoryManager;
    public Item[] itemsToPickUp;

    public void PickupItem(int id)
    {
        Debug.Log(id, itemsToPickUp[id]);
        inventoryManager.AddItemToInventory(itemsToPickUp[id]);
    }

    // only grab info for selected itemslot in inventory
    public void GetSelectedItem()
    {
        // just get the item without using it
        Item selected = inventoryManager.GetSelectedItem(false);
        if (selected == null )
        {
            Debug.Log(" NO ITEM RECIEVED ");
        }
        else
        {
            Debug.Log(" ITEM RECIEVED ");
        }
    }

    //  grab info for selected itemslot in inventory and consume item
    public void UseSelectedItem()
    {
        Item selected = inventoryManager.GetSelectedItem(true);
        if (selected == null)
        {
            Debug.Log(" NO ITEM USED ");
        }
        else
        {
            Debug.Log(" ITEM USED: " + selected);
        }
    }
}
