using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject Item
    {
        get
        {
            
            if (transform.childCount > 0)
            {
                
                
                foreach(Transform child in transform)
                {
                    if(child.GetComponent<InventoryItem>() != null)
                        return child.gameObject;
                }
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged == null) return;

        GameObject droppedItem = DragDrop.itemBeingDragged;
        DragDrop dragDropScript = droppedItem.GetComponent<DragDrop>();

        
        if (Item == null)
        {
            droppedItem.transform.SetParent(transform);
            droppedItem.transform.localPosition = Vector3.zero;
        }
        else
        {
            GameObject currentItem = Item;
            Transform oldParent = dragDropScript.GetStartParent();

            currentItem.transform.SetParent(oldParent);
            currentItem.transform.localPosition = Vector3.zero;

            droppedItem.transform.SetParent(transform);
            droppedItem.transform.localPosition = Vector3.zero;
        }
    }
}