using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform soccerField; // a reference to our soccor field
    public Vector3 moveArea; // the Size of our area where we can move
    public Transform arCamera; // reference to our ar camera

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("New RandomPosition of:" + ReturnRandomPositionOnField());
    }

    // Update is called once per frame
    void Update()
    {
        
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



}
