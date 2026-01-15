using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject resultCanvas;
    public GameLoop gameLoop;

    InputAction aButtonAction;
    InputAction bButtonAction;

    void Start()
    {
        aButtonAction = InputSystem.actions.FindAction("Jump");
        bButtonAction = InputSystem.actions.FindAction("Grab Move");
    }

    void Update()
    {
        if(aButtonAction.IsPressed())
            gameLoop.buttonN += 1;

        if (bButtonAction.IsPressed())
            resultCanvas.SetActive(!resultCanvas.activeSelf);


        if (Input.GetKey(KeyCode.A))
            player.transform.Rotate(0f, -0.5f, 0f);

        if (Input.GetKey(KeyCode.D))
            player.transform.Rotate(0f, 0.5f, 0f);

        if (Input.GetKeyUp(KeyCode.R))
            resultCanvas.SetActive(!resultCanvas.activeSelf);

        if (Input.GetKeyUp(KeyCode.N) && gameLoop.startChange)
            gameLoop.buttonN += 1;
    }
}
