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

    //
    private void Awake()
    {
        //Grabbing the reference.
        PlayerInput = GetComponent<PlayerInput>();

        MoveAction = PlayerInput.actions["Move"]; //"Move" references to the acutual binding in the input system.
    }

    private void Update()
    {
        //Checking for input every frame.
        Movement = MoveAction.ReadValue<Vector2>();
    }
}

//Source: https://www.youtube.com/watch?v=RN3yuCvazL4&list=LL&index=8 