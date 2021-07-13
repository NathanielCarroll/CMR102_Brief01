using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// the different states our character can be in
    /// </summary>
    public enum CharacterStates {Idle,Roaming,Waving,Playing,Fleeing}
    public CharacterStates currentCharacterState; // the current state our character is in

    
    

    public GameManager gameManager; // reference to our game manager
    public Rigidbody rigidBody; // reference to our rigidbody

    // Roaming state variables
    private Vector3 currentTargetPosition; // the target we are currently heading towards
    private Vector3 previousTargetPosition; // the last target were heading towards
    public float moveSpeed = 3; // how fast the character is moving.
    public float minDistanceToTarget = 1; // how close should we get to our target?

    // Idle state variables
    public float idleTime = 2; // once we reach our target position, how long should we wait until we get another position?
    private float currentIdleWaitTime; // the time we are waiting until we can move again.

    // Waving state variable
    public float waveTime = 2; // the time spent waving
    private float currentWaveTime; // The current time to wave untill
    public float distanceToStartWavingFrom = 4f; // the distance that will be checking to see if we are in range to wave at another character
    private CharacterController[] allCharactersInScene; // a collection of references to all characters in our scene
    public float timeBetweenWaves = 5; // the time between when we are allowed to wave again.
    private float currentTimeBetweenWaves; // the current time for our next wave to be initiated

    // fleeing state variables
    public float distanceThresholdOfPlayer = 5; // the distance that is too close for the player to be to the characters

    private Transform currentSoccerBall = null; // reference to the soccer ball
    public GameObject selfIdentifier; // a reference to our identification colour.

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
        CurrentTargetPosition = gameManager.ReturnRandomPositionOnField(); // get a random starting position
        allCharactersInScene = FindObjectsOfType<CharacterController>(); // find references to all characters in our scene
        currentCharacterState = CharacterStates.Roaming; // Set the character by default to start roaming.
        selfIdentifier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Time is: " + Time.time);
        LookAtTargetPosition(); // always look towards the position we are aiming for.
        HandleRoamingState(); // Handles the move state.
        HandleIdleState(); // call our idle state function
        HandleWavingState(); // call our waving state function
        HandleFleeingState(); // call our fleeing state function
        HandlePlayingState(); // call our playing state function
    }




    /// <summary>
    /// handles the Roaming State of our character
    /// </summary>

    private void HandleRoamingState()
    {
        /// if we are still too far away move closer
        if(currentCharacterState == CharacterStates.Roaming && Vector3.Distance(transform.position, CurrentTargetPosition) > minDistanceToTarget)
        {
            Vector3 targetPosition = new Vector3(CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); //the position we want to move towards.
            Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); // the amount we should move towards that positiion
            rigidBody.MovePosition(nextMovePosition);
            currentIdleWaitTime = Time.time + idleTime;
        }
        else if(currentCharacterState == CharacterStates.Roaming) // So check to see if we're roaming.
        {
            currentCharacterState = CharacterStates.Idle; // Start Idling
            
        }
    }

    /// <summary>
    /// handles the idle state of our character
    /// </summary>
    private void HandleIdleState()
    {
        if(currentCharacterState == CharacterStates.Idle)

        { // we must be close enough to target position.
            // we wait a couple of seconds
            // then find a new position
            if(Time.time > currentIdleWaitTime)
            {
                // lets find a new position
                CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
                currentCharacterState = CharacterStates.Roaming; // start roaming again
            }
        }
    }


    /// <summary>
    /// handles fleeing state of our character
    /// </summary>
    private void HandleFleeingState()
    {
        if(currentCharacterState != CharacterStates.Fleeing && gameManager.IsPlayerTooCloseToCharacter(transform,distanceThresholdOfPlayer))
        {
            // We should be fleeing
            currentCharacterState = CharacterStates.Fleeing;
        }
        else if(currentCharacterState == CharacterStates.Fleeing && gameManager.IsPlayerTooCloseToCharacter(transform, distanceThresholdOfPlayer))
        {
            /// if we are still too far away move closer
            if (currentCharacterState == CharacterStates.Fleeing && Vector3.Distance(transform.position, CurrentTargetPosition) > minDistanceToTarget)
            {
                Vector3 targetPosition = new Vector3(CurrentTargetPosition.x, transform.position.y, CurrentTargetPosition.z); //the position we want to move towards.
                Vector3 nextMovePosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime * 1.5f); // the amount we should move towards that positiion
                rigidBody.MovePosition(nextMovePosition);
                currentIdleWaitTime = Time.time + idleTime;
            }
            else
            {
                CurrentTargetPosition = gameManager.ReturnRandomPositionOnField();
            }

        }
        else if(currentCharacterState == CharacterStates.Fleeing && gameManager.IsPlayerTooCloseToCharacter(transform, distanceThresholdOfPlayer) == false)
        {
            // if we are still fleeing, then we want to tansition into our roaming state
            currentCharacterState = CharacterStates.Roaming;
            currentTargetPosition = gameManager.ReturnRandomPositionOnField();
        }
    }



    private void HandlePlayingState()
    {

    }


    /// <summary>
    /// handles the waving state of our character
    /// </summary>
    private void HandleWavingState()
    {
        if(ReturnCharacterTransformToWaveAt() != null && currentCharacterState != CharacterStates.Waving && Time.time > currentTimeBetweenWaves && currentCharacterState != CharacterStates.Fleeing)
        {
            // We should start waving!
            currentCharacterState = CharacterStates.Waving;
            currentWaveTime = Time.time + waveTime; // setup the time we should be waving until.
            CurrentTargetPosition = ReturnCharacterTransformToWaveAt().position; // Set the current target position to the closest transform, so that way we also rotate towards it.
        }
        if(currentCharacterState == CharacterStates.Waving && Time.time > currentWaveTime)
        {
            // Stop waving.
            CurrentTargetPosition = previousTargetPosition; // Resume moving towards our target position.
            currentTimeBetweenWaves = Time.time + timeBetweenWaves; // Set the next time we can wave again
            currentCharacterState = CharacterStates.Roaming; // Start roaming again.
        }
       
    }

   
    /// <summary>
    /// Returns a transform if the are in range of the player to be waved at.
    /// </summary>
    /// <returns></returns>
    
    

    private Transform ReturnCharacterTransformToWaveAt()
    {
        // looping through all the characters in our scene
        for(int i =0; i<allCharactersInScene.Length; i++)
        {
            // if the current element we are up to is not equal to this instance of our character
            if (allCharactersInScene[i] != this)
            {
                // check the distance between them, if they are close enough return that other character
                if (Vector3.Distance(transform.position, allCharactersInScene[i].transform.position) < distanceToStartWavingFrom)
                {
                   
                   // But also let's return the character we should be waving at.
                   return allCharactersInScene[i].transform;
                }
            }
        }
        return null;
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

    /// <summary>
    /// Is called when the soccer ball is spawned in
    /// </summary>
    /// <param name="SoccerBall"></param>
    public void SoccerBallSpawned(Transform SoccerBall)
    {
        currentSoccerBall = SoccerBall; // assign the soccerball to our reference
        CurrentTargetPosition = currentSoccerBall.position; // set our target position to our soccerball
        currentCharacterState = CharacterStates.Roaming; // using our roaming state to start moving towards our soccerball
        selfIdentifier.SetActive(true);
    }


}
