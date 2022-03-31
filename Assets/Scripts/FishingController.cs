using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public Camera mainCamera;
    public LureHandler lure;

    bool mouseIsPressed = false;

    private Vector3 velocity = Vector3.zero;

    public bool draggingAllowed = true;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.GetComponent<CameraController>().cameraLocation == CameraController.CameraLocation.Fishing)
        {
            LureHover();
        }
    }

    private void LureHover()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Check if ray hits anything, output value to hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("LurePlane"))
                {
                    lure.ghostPosition = hit.point;
                }
            }
        }
    }
}
