using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject playerOneCanvas; // reference to the players canvas object
    public Text playerOneScoreText; // reference to the actual we'll bemodifying
    public Color playerOneColor; // the colour of the text we are going to be using

    public GameObject playerTwoCanvas; // reference to the players canvas object
    public Text playerTwoScoreText; // reference to the actual we'll bemodifying
    public Color playerTwoColor; // the colour of the text we are going to be using



    /// <summary>
    /// hide canvas when we first start game until the ball has been dropped
    /// </summary>
    /// <param name="displayScores"></param>
    public void DisplayScores(bool displayScores)

    {
        if (playerOneCanvas == null || playerTwoCanvas == null)
        {
            Debug.LogError("No Canvas Has Been Assigned For This Player");
            return;
        }
        playerOneCanvas.SetActive(displayScores);
        playerTwoCanvas.SetActive(displayScores);
    }


    public void UpdateScores(int playerOneScore, int playerTwoScore)
    {
        if (playerOneScoreText == null || playerTwoScoreText == null)
        {
            Debug.LogError("No Text Has Been Assigned For This Player");
            return;
        }

        playerOneScoreText.color = playerOneColor; // change the color of our text to match the plyer color
        playerOneScoreText.text = playerOneScore.ToString(); // set the text to display the score

        playerTwoScoreText.color = playerTwoColor; // change the color of our text to match the plyer color
        playerTwoScoreText.text = playerTwoScore.ToString(); // set the text to display the score
    }


}

