using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DraggableItemHandler : MonoBehaviour
{
    public GameObject targetPlane;
    public GameObject targetObject;
    private Vector3 homePosition;
    private Vector3 homeRotation;
    public bool dragEnabled;

    // Start is called before the first frame update
    void Start()
    {
        homePosition = transform.position;
        homeRotation = transform.eulerAngles;
    }

    public void EnableDrag()
    {
        dragEnabled = true;

        if (GetComponent<LookAtConstraint>() != null)
        {
            GetComponent<LookAtConstraint>().weight = 1.0F;
        }

        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetBool("Uncorked", true);
        }
       
    }


    public void DisableDrag()
    {
        if (GetComponent<LookAtConstraint>() != null)
        {
            GetComponent<LookAtConstraint>().weight = 0.0F;
        }

        if (GetComponentInChildren<Animator>() != null)
        {
            GetComponentInChildren<Animator>().SetBool("Uncorked", false);
        }

        dragEnabled = false;

        transform.position = homePosition;
        transform.eulerAngles = homeRotation;

    }

    private void Update()
    {
        if (dragEnabled)
        {
            transform.LookAt(targetObject.GetComponent<Transform>());
        }
    }

    public string GetTooltipText()
    {
        if (GetComponent<LookAtConstraint>() != null)
        {
            return "Trusty stirring oar. Mix ingredients in the cauldron and see what happens.";
        }
        else if (GetComponent<ReagentHandler>() != null)
        {
            return string.Format("{0}\n{1}",GetComponent<ReagentHandler>().reagentName, GetComponent<ReagentHandler>().tooltipText);
        }
        else if (GetComponent<TooltipHandler>() != null)
        {
            return string.Format("{0}\n{1}", GetComponent<TooltipHandler>().itemName, GetComponent<TooltipHandler>().tooltipText);
        }
        else
        {
            return "";
        }
    }
}
