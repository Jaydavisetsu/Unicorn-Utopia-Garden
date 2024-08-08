using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Eliminates the need of having to find the script or using "get component" to detect in input.    

    public static Vector2 Movement; //Variable is not tied to this specific instant of script, so it's its own variable.

    //Setting input action variable thorugh reference.
    private PlayerInput PlayerInput; 

    //Input action variable.
    private InputAction MoveAction;

    private static InputManager instance;
    private bool interactPressed = false;
    private bool submitPressed = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

        //Grabbing the reference.
        PlayerInput = GetComponent<PlayerInput>();

        MoveAction = PlayerInput.actions["Move"]; //"Move" references to the acutual binding in the input system.
    }

    private void Update()
    {
        //Checking for input every frame.
        Movement = MoveAction.ReadValue<Vector2>();
    }

    public static InputManager GetInstance()
    {
        return instance;
    }


    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }
}

//Source: https://www.youtube.com/watch?v=RN3yuCvazL4&list=LL&index=8 