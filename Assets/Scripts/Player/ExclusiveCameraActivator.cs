using UnityEngine;

public class ExclusiveCameraActivator : MonoBehaviour
{
    void Start()
    {
        // Get the camera attached to this object or its children
        Camera childCamera = GetComponentInChildren<Camera>(true); // include inactive
        Canvas childCanvas = GetComponentInChildren<Canvas>(true); // include inactive

        // Disable all cameras in the scene
        Camera[] allCameras = FindObjectsOfType<Camera>(true);
        foreach (Camera cam in allCameras)
        {
            if (cam != childCamera)
            {
                cam.enabled = false;
            }
        }

        // Enable the child camera
        if (childCamera != null)
        {
            childCamera.enabled = true;
        }

        // Enable the child canvas
        if (childCanvas != null)
        {
            childCanvas.gameObject.SetActive(true);
        }
    }
}
