using UnityEngine;
using System.Collections;

public class NPCRoaming : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Speed at which the NPC moves
    public float idleTime = 2.0f;  // Time the NPC spends idling
    public float walkRadius = 5.0f; // Radius within which the NPC roams

    private Vector3 targetPosition; // Target position for the NPC to walk to
    private bool isWalking = false; // Whether the NPC is currently walking
    private bool isIdle = false;    // Whether the NPC is currently idling

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Roam());
    }

    private IEnumerator Roam()
    {
        while (true)
        {
            if (!isWalking && !isIdle)
            {
                // Choose a random point within the walk radius
                Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
                randomDirection += transform.position;
                randomDirection.y = transform.position.y;

                // Set the target position and start walking
                targetPosition = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
                isWalking = true;
                animator.SetBool("IsWalking", true); // Set the walking animation
            }

            // Move the NPC towards the target position
            if (isWalking)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Check if the NPC has reached the target position
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isWalking = false;
                    isIdle = true;
                    animator.SetBool("IsWalking", false); // Set the idling animation

                    // Start idling for a certain amount of time
                    yield return new WaitForSeconds(idleTime);

                    isIdle = false;
                }
            }

            yield return null;
        }
    }
}

