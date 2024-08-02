using UnityEngine;

public class RandomRoaming : MonoBehaviour
{
    public Animator animator;
    public float speed = 2.0f;
    public float changeDirectionInterval = 2.0f;

    private Vector2 movementDirection;
    private float changeDirectionTimer;
    private bool isFacingRight = true;

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;

        if (changeDirectionTimer <= 0)
        {
            ChangeDirection();
        }

        Move();
        UpdateAnimator();
    }

    void ChangeDirection()
    {
        // Randomly change direction to left, right, or idle
        int direction = Random.Range(0, 3);

        switch (direction)
        {
            case 0: // Move Left
                movementDirection = Vector2.left;
                isFacingRight = false;
                break;
            case 1: // Move Right
                movementDirection = Vector2.right;
                isFacingRight = true;
                break;
            default: // Idle
                movementDirection = Vector2.zero;
                break;
        }

        changeDirectionTimer = changeDirectionInterval;
    }

    void Move()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        bool isMoving = movementDirection != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isFacingRight", isFacingRight);
    }
}