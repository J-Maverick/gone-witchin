using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [Tooltip("Name of item.")]
    public string itemName;

    [Tooltip("Price of the item at the store.")]
    public int shopValue = 10;

    [TextArea]
    public string tooltipText = "";

}
