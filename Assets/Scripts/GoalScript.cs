using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public int playerNumber = 1; // this is the number our player is
    public GameManager gameManager; // a reference to our game manager



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SoccerBall")
        {
            Debug.Log("Goal Scored");

            gameManager.IncreasePlayerScore(playerNumber);
            //I want to increase our characters score
        }
    } 
       
       
     
}
