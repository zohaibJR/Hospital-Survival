using UnityEngine;
using UnityEngine.UI;

public class DoorOpenNew : MonoBehaviour
{
    public Animator doorAnimator;
    public GameObject DoorButton;
    public bool isDoorClosed = true;

    void Start()
    {
        Debug.Log("<color=green>DoorOpen script started</color>");

        if (doorAnimator == null)
            doorAnimator = GetComponentInParent<Animator>();

        if (doorAnimator == null)
            Debug.LogError("Animator not found!");

        // Auto-assign button click event
        if (DoorButton != null)
        {
            Button btn = DoorButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(OpenDoor);
            }
        }

        isDoorClosed = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            Debug.Log("Player is near the door");
            DoorButton?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Capsule"))
        {
            DoorButton?.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        if (doorAnimator == null)
        {
            Debug.LogError("Cannot open door — Animator missing!");
            return;
        }

        if (isDoorClosed)
        {
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
