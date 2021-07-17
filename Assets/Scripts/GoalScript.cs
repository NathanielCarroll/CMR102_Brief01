using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public int playerNumber = 1; // this is the number our player is
    public GameManager gameManager; // a reference to our game manager


    public GameObject fireWorksPrefab; // a reference to our firework prefab
    public Transform leftFireWorksPosition; // an empty transform to the left of our goal
    public Transform rightFireWorksPosition; // an empty transform to the right of our goal


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SoccerBall")
        {
            Debug.Log("Goal Scored");

            gameManager.IncreasePlayerScore(playerNumber);

            //I want to increase our characters score


            // spawn in our fireworks at the left and right positions respectively and parent them to our AR parent
            GameObject clone = Instantiate(fireWorksPrefab, leftFireWorksPosition.position, fireWorksPrefab.transform.rotation, gameManager.aRContentParent);
            Destroy(clone, 5);

            clone = Instantiate(fireWorksPrefab, rightFireWorksPosition.position, fireWorksPrefab.transform.rotation, gameManager.aRContentParent);
            Destroy(clone, 5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = playerNumber == 1 ? Color.red : Color.blue; // a short hand if statement
        Gizmos.DrawCube(transform.position, transform.localScale); // show our cube
    }

}
