using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer", 0);
        switch (selectedPlayer)
        {
            case 0:
                SpeedMove = 5f;
                break;
            case 1:
                SpeedMove = 6.5f;
                break;
            case 2:
                SpeedMove = 8f;
                break;
            default:
                SpeedMove = 5f;
                break;
        }

        Debug.Log("Selected Player: " + selectedPlayer + " | Speed: " + SpeedMove);
    }

    void Update()
    {
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * SpeedMove * Time.deltaTime);
    }
}
