using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entering");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exiting");
    }

}