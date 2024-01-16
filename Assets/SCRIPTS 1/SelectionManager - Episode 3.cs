using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectionManager : MonoBehaviour
{

    public GameObject interaction_Info_UI;
    public bool onTarget;
    Text interaction_text;
    public GameObject selectedObject;
    public static SelectionManager Instance { get; set; }
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }
    private void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Update()
    {
        //sends a ray in the middle of the screen and checks if it hits something
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // if object hit
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject; // this is the object we are pointing at (makes sure that you dont select several objects all at once)
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else 
            { 
                interaction_Info_UI.SetActive(false);
                onTarget = false;
            }

        }
        else // if no object hit
        {
            interaction_Info_UI.SetActive(false);
            onTarget = false;
        }
    }
}
