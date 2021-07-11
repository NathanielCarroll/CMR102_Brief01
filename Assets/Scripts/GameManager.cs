using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform soccorField; // a reference to our soccor field
    public Vector3 moveArea; // the Size of our area where we can move

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
        float yposition = soccorField.position.y; // our soccor fields y transform position
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
        if(soccorField == null)
        {
            return;
        }
        Gizmos.color = Color.red; // sets my gizmo to red
        Gizmos.DrawCube(soccorField.position + new Vector3(0,0.5f,0), moveArea); // draws a cube at the soccor fields position, and to the size of our move area.
    }

    

}
