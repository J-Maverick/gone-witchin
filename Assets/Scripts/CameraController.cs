using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PotionCameraAnchor;
    public Transform FishingCameraAnchor;
    public enum CameraLocation {PotionCrafting, Fishing}
    public CameraLocation startingLocation;

    public float cameraMoveTime = 0.3f;
    public float cameraRotationSlowingFactor = 10f;

    private CameraLocation cameraLocation;
    private Transform target;

    private Vector3 velocity = Vector3.zero;

    private float moveTime = 0f;
    private float normalizedSlerpTime = 0f;

    private bool isMoving = false;

    private void Start()
    {
        if (startingLocation == CameraLocation.PotionCrafting)
        {
            ToPotionCrafting(damp:false);
            cameraLocation = CameraLocation.PotionCrafting;
        }
        else if (startingLocation == CameraLocation.Fishing)
        {
            ToFishing(damp:false);
            cameraLocation = CameraLocation.Fishing;
        }
    }

    private void Update()
    {
        updateTime();
        // Smooths the motion as the camera moves from one location to another
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, cameraMoveTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, normalizedSlerpTime);

        checkMoving();
    }

    private void updateTime()
    {
        // Time used for smoothing rotation
        moveTime += Time.deltaTime;
        normalizedSlerpTime = moveTime / (cameraMoveTime * cameraRotationSlowingFactor);
    }

    private void checkMoving()
    {
        if (velocity.magnitude > 1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void SwapPositions()
    {
        if (!isMoving)
        {
            if (cameraLocation == CameraLocation.PotionCrafting)
            {
                ToFishing();
                cameraLocation = CameraLocation.Fishing;
            }
            else
            {
                ToPotionCrafting();
                cameraLocation = CameraLocation.PotionCrafting;
            }
        }
    }

    void ToPotionCrafting(bool damp=true)
    {
        Debug.Log("Moving Camera to Potion-Making");
        if (!damp)
        {

            transform.SetPositionAndRotation(PotionCameraAnchor.position, PotionCameraAnchor.rotation);
        }
        target = PotionCameraAnchor;
        moveTime = 0.0f;
        normalizedSlerpTime = 0f;
    }

    void ToFishing(bool damp = true)
    {
        Debug.Log("Moving Camera to Fishing");
        if (!damp)
        { 
            transform.SetPositionAndRotation(FishingCameraAnchor.position, FishingCameraAnchor.rotation);
        }
        target = FishingCameraAnchor;
        moveTime = 0.0f;
        normalizedSlerpTime = 0f;
    }
}
