using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform soccerField; // a reference to our soccor field
    public Vector3 moveArea; // the Size of our area where we can move
    public Transform arCamera; // reference to our ar camera

    public GameObject soccerballPrefab; // a reference to the soccer ball in our scene
    private GameObject currentSoccerBallInstance; // the current soccer ball that has been spawned in
    public Transform aRContentParent; // reference to the overall parent of the ar content

    public int playerOneScore;
    public int playerTwoScore;

    public UIManager uiManager; // a reference to our UI Manager

    

    private bool areCharactersRunningAway = false; // are there characters currently running away from the player


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("New RandomPosition of:" + ReturnRandomPositionOnField());
        playerOneScore = 0;
        playerTwoScore = 0; // reset our players score
        uiManager.DisplayScores(false); // hide our canvases to start with
        uiManager.UpdateScores(playerOneScore, playerTwoScore); // Update our players scores 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /// <summary>
    /// Increase goals scored by players by 1
    /// </summary>
    /// <param name="playerNumber"></param>
    public void IncreasePlayerScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            playerOneScore++;
        }
        else if(playerNumber == 2)
        {
            playerTwoScore++;
        }
        ResetSoccerBall();
        uiManager.UpdateScores(playerOneScore, playerTwoScore); // updates the ui score to show our current values
        
    }

    /// <summary>
    /// resets the positions and velicities
    /// </summary>
    private void ResetSoccerBall()
    {
        currentSoccerBallInstance.GetComponent<Rigidbody>().velocity = Vector3.zero; // reset the velocity of the ball
        currentSoccerBallInstance.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // reset the angular velocity
        currentSoccerBallInstance.transform.position = ReturnRandomPositionOnField(); // reset the position of the ball
    }



    /// <summary>
    /// Returns a random position within our move area/
    /// </summary>
    /// <returns></returns>
    public Vector3 ReturnRandomPositionOnField()
    {
        float xPosition = Random.Range(-moveArea.x / 2, moveArea.x / 2); // gives us a random number between negative moveArea X and positive move AreaX
        float yposition = soccerField.position.y; // our soccor fields y transform position
        float zposition = Random.Range(-moveArea.z / 2, moveArea.z / 2); // gives us a random number between negative moveArea Z and positive move AreaZ

        return new Vector3(xPosition, yposition, zposition);

   
    }

    /// <summary>
    /// This is a debug function, that lets us draw objects in our scene view, its not vieable in the game view
    /// </summary>
    /// <returns></returns>
     private void OnDrawGizmosSelected()
    {
        // if the user hasn't put a soccor field in, just get out of this function.
        if(soccerField == null)
        {
            return;
        }
        Gizmos.color = Color.red; // sets my gizmo to red
        Gizmos.DrawCube(soccerField.position + new Vector3(0,0.5f,0), moveArea); // draws a cube at the soccer fields position, and to the size of our move area.
    }

    /// <summary>
    /// Return true of false if we are too close, or not close enough to AR camera
    /// </summary>
    /// <param name="character"></param>
    /// <param name="distanceThreshold"></param>
    /// <returns></returns>
    public bool IsPlayerTooCloseToCharacter(Transform character, float distanceThreshold)
    {
        if(Vector3.Distance(arCamera.position,character.position) <= distanceThreshold)
        {
            // returns true if we are too close
            return true;
        }
        else
        {
            // returns false if we are too far away
            return false;
        }
    }

    /// <summary>
    /// Spawns in the soccer ball based on the position provided. if the soccer ball already exists in the world we just want to move it to that new position
    /// </summary>
    /// <param name="positionToSpawn"></param>
    public void SpawnOrMoveSoccerBall(Vector3 positionToSpawn)
    {
        if(soccerballPrefab == null)
        {
            Debug.LogError("Something is wrong there is no soccer ball assigned in the inspector");
            return;
        }

        // if the soccerball isn't assigned in the world yet
        if(currentSoccerBallInstance == null)
        {
            // spawn in and store a reference to our soccerball, and parent it to our ar content parent
            currentSoccerBallInstance = Instantiate(soccerballPrefab, positionToSpawn, soccerballPrefab.transform.rotation, aRContentParent);
            currentSoccerBallInstance.GetComponent<Rigidbody>().velocity = Vector3.zero; // sets the velocity of the soccerball to 0
            currentSoccerBallInstance.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // sets the angular velocity of the soccerball to 0
            AlertCharactersToSoccerBallSpawningIn(); // tell everyone the ball is spawned
        }
        else
        {
            currentSoccerBallInstance.transform.position = positionToSpawn; // move our soccer ball to the position to spawn
            currentSoccerBallInstance.GetComponent<Rigidbody>().velocity = Vector3.zero; // sets the velocity of the soccerball to 0
            currentSoccerBallInstance.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // sets the angular velocity of the soccerball to 0
        }
    }

    /// <summary>
    /// Finds all the characters in the scene and loops through them and tells them that there is a soccerball
    /// </summary>
    /// <returns></returns>
    private void AlertCharactersToSoccerBallSpawningIn()
    {
        CharacterController[] mice = FindObjectsOfType<CharacterController>(); // find all instances of our character controller
        for(int i=0; i<mice.Length; i++)
        {
            mice[i].SoccerBallSpawned(currentSoccerBallInstance.transform);
        }

        uiManager.DisplayScores(true); // display our score on our goals
    }

}
