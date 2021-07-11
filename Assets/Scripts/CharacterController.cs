using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 3; // how fast the character is moving.
    public float minDistanceToTarget = 1; // how close should we get to our target?
    public float idleTime = 2; // once we reach our target position, how long should we wait until we get another position?
    private float currentIdlewaitTime; // the time we are waiting until we can move again.

    public GameManager gameManager; // reference to our game manager
    public Rigidbody rigidBody; // reference to our rigidbody


    private Vector3 currentTargetPosition; // the target we are currently heading towards
    private Vector3 previousTargetPosition; // the last target were heading towards

    /// <summary>
    /// Returns the currentTargetPosition
    /// And Sets the new current position
    /// </summary>
    private Vector3 CurrentTargetPosition 
    {
        get
        {
            return currentTargetPosition; // gets the current value
        }
        set
        {
            previousTargetPosition = currentTargetPosition; // assign our current target position to our previous target position
            currentTargetPosition = value; // assign the new value to our current target position
        }



    }



    // Start is called before the first frame update
    void Start()
    {
        CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
    }

    // Update is called once per frame
    void Update()



    {
        LookAtTargetPosition(); // always look towards the position we are aiming for.
        /// if we are still too far away move closer
        if (Vector3.Distance(transform.position, CurrentTargetPosition) > minDistanceToTarget)
        {
            Vector3 targetPosition = new Vector3(CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); //the position we want to move towards.
            Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); // the amount we should move towards that positiion
            rigidBody.MovePosition(nextMovePosition);
            currentIdlewaitTime = Time.time + idleTime;
        }
        else
        {
            // we must be close enough to target position.
            // we wait a couple of seconds
            // then find a new position
            if(Time.time > currentIdlewaitTime)
            {
                // lets find a new position
                CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
            }
        }

    }
    /// <summary>
    /// Rotates our character to always face the direction we are heading.
    /// </summary>
    private void LookAtTargetPosition()
    {
        Vector3 directionToLookAt = CurrentTargetPosition - transform.position; //get the direction we should be lookin at
        directionToLookAt.y = transform.position.y; //don't change the Y position
        Quaternion rotationOfDirection = Quaternion.LookRotation(directionToLookAt); //  get a rotation that we can use to look towards
        transform.rotation = rotationOfDirection; // set our current rotation to our rotation to face towards.
    }


    private void OnDrawGizmosSelected()
    {
        // draw a blue sphere of the position we are moving towards
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(CurrentTargetPosition, 0.5f);
    }

}
