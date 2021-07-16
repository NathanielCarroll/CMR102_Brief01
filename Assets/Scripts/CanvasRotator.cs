using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePosition = transform.position - Camera.main.transform.position; // get the relative direction of the canvas to the players camera
        relativePosition.y = 0; // y axis is zero because we dont want to be rotating on that axis
        Quaternion rotation = Quaternion.LookRotation(relativePosition); // create a rotation to look at the direction we are wanting
        transform.rotation = rotation; // set our new rotation to our current rotation
    }
}
