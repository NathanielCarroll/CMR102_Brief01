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
        Vector3 targetPosition = new Vector3 (CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); //the position we want to move towards.
        Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); // the amount we should move towards that positiion
        rigidBody.MovePosition(nextMovePosition);
    }
}
