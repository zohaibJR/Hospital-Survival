using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject DoorButton;
    public bool isDoorClosed = true;

    void Start()
    {
        Debug.Log("<color=green>DoorOpen script started</color>");
        doorAnimator = GetComponent<Animator>();
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
        if (isDoorClosed)
        {
            doorAnimator.SetBool("OpenDoor", true);
            isDoorClosed = false;
        }
        else
        {
            doorAnimator.SetBool("OpenDoor", false);
            isDoorClosed = true;
        }
    }
}
