using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PotionCameraAnchor;
    public Transform FishingCameraAnchor;
    public enum CameraLocation {PotionCrafting, Fishing}
    public CameraLocation startingLocation;
    private CameraLocation cameraLocation;

    private void Start()
    {
        if (startingLocation == CameraLocation.PotionCrafting)
        {
            ToPotionCrafting();
            cameraLocation = CameraLocation.PotionCrafting;
        }
        else if (startingLocation == CameraLocation.Fishing)
        {
            ToFishing();
            cameraLocation = CameraLocation.Fishing;
        }
    }

    public void SwapPositions()
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

    void ToPotionCrafting()
    {
        transform.SetPositionAndRotation(PotionCameraAnchor.position, PotionCameraAnchor.rotation);
    }
    void ToFishing()
    {
        transform.SetPositionAndRotation(FishingCameraAnchor.position, FishingCameraAnchor.rotation);
    }
}
