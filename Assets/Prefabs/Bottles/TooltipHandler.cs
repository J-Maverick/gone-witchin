using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    public string tooltipText = "";
    public string itemName = "";


    public string GetTooltipText()
    {
        return string.Format("{0}\n{1}", itemName, tooltipText);
    }
}
