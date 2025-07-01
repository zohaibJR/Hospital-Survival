using UnityEngine;

public class ActivateCamera : MonoBehaviour
{
    public GameObject OtherCamera;
    public GameObject PlayerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateSceneCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSceneCamera()
    {
        OtherCamera.SetActive(false);
        PlayerCamera.SetActive(true);
    }
}
