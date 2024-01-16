using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolScreenUI;

    public List<string> inventoryItemList = new List<string>();

    // category buttons
    Button toolsBTN;

    //action buttons
    Button craftAxeBTN;

    //requirements text
    Text AxeReq1, AxeReq2;

    public bool isOpen;

    // All blueprints

    public static CraftingSystem Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(craftingScreenUI == null)
        {
            Debug.Log("craftingscreen null");
        }
        // map all references to the buttons at the start of the game. 
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });


        //axe 
        craftAxeBTN = toolScreenUI.transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });
    }

    void CraftAnyItem()
    {
        // Add item in to the inventory
        // InventorySystem.Instance.AddToInventory();

        // subtract required items from the inventory
        //InventorySystem.Instance.RemoveItem();


        // refresh list
        //InventorySystem.Instance.RecalculateList();

        //RefreshNeededItems();
    }

    private void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            Debug.Log("c is pressed");
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
}
