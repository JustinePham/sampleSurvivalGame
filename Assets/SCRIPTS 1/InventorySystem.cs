using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public static int MAX = 30;

    public ItemSlot[] InventorySlots;
    public GameObject InventoryItemPrefab;
    private int slotsFilled = 0;

    public static InventorySystem Instance { get; set; }
    public GameObject inventoryScreenUI;
    public bool isOpen;

    // could be subject to change later when implementing settings functionality
    public KeyCode INVENTORY_TOGGLE = KeyCode.I;

    int selectedSlot = -1;

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            InventorySlots[selectedSlot].Deselect();
        }
        InventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    // input: Item: ScriptableObject 
    // output: returns true if item added to inventory slot, returns false if all slots are filled.
    // ---
    // it first checks if there are any slots with the same item, then it will stack if conditions checked
    // if no same items in inventory, then add it to an unfilled slot
    public bool AddItemToInventory(Item item)
    {
        //Debug.Log(InventorySlots.Length);

        // Check if slot has the same item with the quantity lower than MAX QUANTITY
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventoryItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.quantity < MAX && itemInSlot.item.stackable == true)
            {
                itemInSlot.quantity++;
                itemInSlot.RefreshCount();
                return true;
            }
         }
        // for adding brand new item
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            ItemSlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    // returns true if all slots are filled. 
    public bool isFull()
    {
        return slotsFilled == 30;
    }

    // input: Item:ScriptableObject, ItemSlot
    // ouput: void
    //---- 
    // instantiates item from prefab GO in items folder
    // assigns it to parent slot
    // gets InventoryItem component and uses it to set the image.sprite for the item
    void SpawnNewItem(Item item, ItemSlot slot)
    {
        GameObject itemToAdd = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = itemToAdd.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    private void Awake()
    {
        Debug.Log("awake here");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        ChangeSelectedSlot(0);
        isOpen = false;
    }

    void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int value);
            if (isNumber)
            {
                if (value < 7 && value > 0)
                {
                    ChangeSelectedSlot(value-1);
                }
            }
            else
            {
                // could possibly refactor
                if (Input.GetKeyDown(INVENTORY_TOGGLE) && !isOpen)
                {
                    inventoryScreenUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    isOpen = true;
                }
                else if (Input.GetKeyDown(INVENTORY_TOGGLE) && isOpen)
                {
                    inventoryScreenUI.SetActive(false);
                    isOpen = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
     
    public Item GetSelectedItem(bool use)
    {
        ItemSlot slot = InventorySlots[selectedSlot]; // gets the selected slot
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot == null)
        {
            if (use == true)
            {
                itemInSlot.quantity--;
                // if item quantity hits 0
                if (itemInSlot.quantity <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
                return itemInSlot.item;
            }
        }
        return null;
    }

    public void RemoveItem(string itemName,int number)
    {
        int counter = 0;
        // implement 
    }

}