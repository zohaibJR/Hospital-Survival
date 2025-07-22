using UnityEngine;

public class DoorOpenNew : MonoBehaviour
{

    public Animator doorAnimator;
    public GameObject DoorButton;
    public bool isDoorClosed = true;

    void Start()
    {
        Debug.Log("<color=green>DoorOpen script started</color>");

        if (doorAnimator == null)
        {
            // Get Animator from parent (DoorPivot)
            doorAnimator = GetComponentInParent<Animator>();
        }

        if (doorAnimator == null)
        {
            Debug.LogError("<color=red>Animator not found on parent! Assign it in Inspector or fix hierarchy.</color>");
        }

        isDoorClosed = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            Debug.Log("<color=green>Player is near the door</color>");
            DoorButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            DoorButton.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        Debug.Log("<color=green>OpenDoor method called</color>");
        if (isDoorClosed)
        {
            Debug.Log("<color=green>Door is currently closed, opening it</color>");
            doorAnimator.SetBool("isDoorOpenNew", true);
            isDoorClosed = false;
        }
        else
        {
            doorAnimator.SetBool("isDoorOpenNew", false);
            isDoorClosed = true;
        }
    }
}
