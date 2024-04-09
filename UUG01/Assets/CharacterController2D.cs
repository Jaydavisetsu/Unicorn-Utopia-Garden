using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

//To ensure that if you want to later on add this CharacterController to any other object, then it will automatically add the Rigidbody2D or throw a warning. 
[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    //Defining serialized variable speed of type float.
    [SerializeField] float speed = 2f;

    //To move character, this is updating character velocity on fixed update.
    Vector2 motionVector;

    //Cachine the reference to the animator
    Animator animator;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Assign the values to the motion vector based on the horizontal adn vertical axis input.
        motionVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        animator.SetFloat("Horizontal",
            Input.GetAxisRaw("Horizontal"));

        animator.SetFloat("Vertical",
            Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Assiging motion vector to the rigidbody velocity.
        rigidbody2d.velocity = motionVector * speed; 
    }
}
