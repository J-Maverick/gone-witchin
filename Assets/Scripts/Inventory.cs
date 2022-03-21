using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public GameObject itemPrefab = null;
    public int nItems;
    public enum ItemType { Potion, Reagent };
    public ItemType itemType;
}

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public enum ItemType { Potion, Reagent };

    public void AddItem(GameObject itemToAdd, int numberOfItems)
    {

        if (itemToAdd.GetComponent<ReagentHandler>() != null)
        {
            ItemType newItemType = ItemType.Reagent;
        }
        if (itemToAdd.GetComponent<PotionHandler>() != null)
        {
            ItemType newItemType = ItemType.Potion;
        }

        foreach (Item heldItem in items)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
