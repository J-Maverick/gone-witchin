using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DragAndDrop : MonoBehaviour
{
    public GameObject fitch;
    public GameObject tooltip;
    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private float mouseDragSpeed = 0.1F;

    bool mouseIsPressed = false;

    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;

    public bool draggingAllowed = true;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        Debug.Log("Ray shoot");
        // Check if ray hits anything, output value to hit
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Ray SHOOTMA");
            if (hit.collider != null)
            {
                Debug.Log("Ray Hit");
                if (hit.collider.gameObject.CompareTag("Draggable") && !mouseIsPressed)
                {
                    string tooltipText = "";
                    tooltip.gameObject.SetActive(true);
                    if (hit.collider.gameObject.GetComponent<DraggableItemHandler>() != null)
                    {
                        tooltipText = hit.collider.gameObject.GetComponent<DraggableItemHandler>().GetTooltipText();

                    }
                    else if (hit.collider.gameObject.GetComponent<TooltipHandler>() != null)
                    {
                        tooltipText = hit.collider.gameObject.GetComponent<TooltipHandler>().GetTooltipText();
                    }

                    tooltip.transform.position = Mouse.current.position.ReadValue();
                    tooltip.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = tooltipText;

                    Debug.Log(string.Format("Tooltip: {0}", tooltipText));
                }
                else
                {
                    tooltip.gameObject.SetActive(false);
                }
            }
            else
            {
                tooltip.gameObject.SetActive(false);
            }
        }
        else
        {
            tooltip.gameObject.SetActive(false);
        }
    }
    // Fire a ray from the camera towards the mouse press
    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        // Check if ray hits anything, output value to hit
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Draggable") && draggingAllowed)
                {
                    StartCoroutine(DragUpdate(hit.collider.gameObject));
                }
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.GetComponent<DraggableItemHandler>().targetPlane.transform.position, mainCamera.transform.position) - 1f;
        // If currently clicking
        while (mouseClick.ReadValue<float>() != 0)
        {
            clickedObject.GetComponent<DraggableItemHandler>().EnableDrag();
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
            fitch.GetComponent<Animator>().SetBool("isPouring", true);
            mouseIsPressed = true;
            if (!draggingAllowed)
            {
                break;
            }
            yield return null;
        }

        clickedObject.GetComponent<DraggableItemHandler>().DisableDrag();
        fitch.GetComponent<Animator>().SetBool("isPouring", false);
        mouseIsPressed = false;
    }
}