using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    //The move speed for the character.
    [SerializeField] private float MoveSpeed = 5f;
    private Vector2 Movementm;
    private Rigidbody2D Rigidbody;
    private Animator Animator;

    //Good practice of setting up constant strings for animation parameters.
    private const string Horizontal = "Horizontal";
    private const string Veritcal = "Vertical";

    //Parameters for the character idle states
    private const string LastHorizontal = "LastHorizontal";
    private const string LastVertical = "LastVertical";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    public void LoadData(GameData data) //Method from IDataPersistence.
    {
        this.transform.position = data.PlayerPosition;
    }

    public void SaveData(GameData data) //Method from IDataPersistence.
    {
        data.PlayerPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        Movementm.Set(InputManager.Movement.x, InputManager.Movement.y); //Moving vecotr2.

        //Setting the velocity directly on the rigid body.
        Rigidbody.velocity = Movementm * MoveSpeed; //Telling it what speed to be at and it will be the same regardless of the frame rate.

        Animator.SetFloat(Horizontal, Movementm.x);
        Animator.SetFloat(Veritcal, Movementm.y);

        if (Movementm != Vector2.zero) //As long as character is moving in some direction, then it will set the idle state on vertical and horizontal. If it is not moving, then it will keep previous set idle state.
        {
            Animator.SetFloat(LastHorizontal, Movementm.x);
            Animator.SetFloat(LastVertical, Movementm.y);
        }
    }
}

//Source for everything except load and save: https://www.youtube.com/watch?v=RN3yuCvazL4&list=LL&index=8
//Source for load and save: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15