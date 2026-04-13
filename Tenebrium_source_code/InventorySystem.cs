using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [Header("UI Settings")]
    public GameObject inventoryScreenUI;
    public bool isOpen;

    [Header("Inventory Slots")]
    public List<GameObject> slotList = new List<GameObject>();

    
    
    [System.Serializable]
    public struct ItemDefinition
    {
        public string idName;       
        public GameObject prefab;   
    }

    
    [Header("Item Database")]
    public ItemDefinition[] allGameItems; 

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

    private void Start()
    {
        isOpen = false;
        inventoryScreenUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryScreenUI.SetActive(isOpen);
    }

    public bool AddToInventory(string itemName)
    {
        
        GameObject prefabToSpawn = null;

        foreach (var itemDef in allGameItems)
        {
            if (itemDef.idName == itemName)
            {
                prefabToSpawn = itemDef.prefab;
                break;
            }
        }

        if (prefabToSpawn == null)
        {
            Debug.LogError("Nu am gasit item-ul: " + itemName);
            return false;
        }

        
        for (int i = 0; i < slotList.Count; i++)
        {
            InventoryItem itemInSlot = slotList[i].GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
                itemInSlot.idName == itemName && 
                itemInSlot.currentStackSize < itemInSlot.maxStackSize)
            {
                itemInSlot.AddToStack(1);
                Debug.Log("Stacked: " + itemName);
                return true;
            }
        }

        
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].GetComponentInChildren<InventoryItem>() == null)
            {
                GameObject newItem = Instantiate(prefabToSpawn, slotList[i].transform);
                
                
                newItem.transform.localPosition = Vector3.zero;
                newItem.transform.localScale = Vector3.one;

                
                InventoryItem newItemScript = newItem.GetComponent<InventoryItem>();
                if (newItemScript != null)
                {
                    newItemScript.idName = itemName;
                    newItemScript.currentStackSize = 1;
                    newItemScript.UpdateUI();
                }

                Debug.Log("New Item: " + itemName);
                return true;
            }
        }

        Debug.Log("Inventory Full!");
        return false;
    }
}