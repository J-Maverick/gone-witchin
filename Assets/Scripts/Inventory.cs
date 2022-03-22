using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;
    public int currentValue;

    // Method used for adding items to inventory
    public void AddItem(Item newItem)
    {
        currentValue = currentValue + newItem.shopValue * newItem.nItems;
        bool isInInventory = false;
        foreach (Item ownedItem in itemList)
        {
            if (newItem.itemID == ownedItem.itemID)
            {
                ownedItem.nItems += newItem.nItems;
                isInInventory = true;
                break;
            }
        }
        
        if (!isInInventory)
        {
            itemList.Add(newItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
