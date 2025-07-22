using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorAnimator; // Assign your DoorPivot animator
    public GameObject buttonUI;   // Assign OpenDoorButton (UI)
    private bool isPlayerNear = false;

    void Start()
    {
        buttonUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            isPlayerNear = true;
            buttonUI.SetActive(true);

            // Subscribe this button to this door
            buttonUI.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonUI.GetComponent<Button>().onClick.AddListener(OpenDoor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            isPlayerNear = false;
            buttonUI.SetActive(false);
        }
    }

    void OpenDoor()
    {
        if (isPlayerNear)
        {
            Debug.Log("Hehehe Boyee");
            doorAnimator.SetBool("isOpen", true);
            buttonUI.SetActive(false); // Hide button after opening
        }
    }
}
