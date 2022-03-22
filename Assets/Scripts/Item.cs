using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab = null;
    public int nItems;
    public enum ItemType { Potion, Reagent };
    public ItemType itemType;
    public string itemID;
    public int shopValue;

}
