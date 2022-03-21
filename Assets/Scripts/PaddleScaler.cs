using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScaler : MonoBehaviour
{
    public Vector3 dragScaling;
    private Vector3 initialScaling;

    private void Start()
    {
        initialScaling = transform.localScale;
    }

    private void Update()
    {
        if (GetComponent<DraggableItemHandler>().dragEnabled)
        {
            transform.localScale = dragScaling;
        }
        else
        {
            transform.localScale = initialScaling;
        }
    }
}
