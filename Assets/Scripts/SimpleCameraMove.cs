using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMove : MonoBehaviour
{
    public float moveSpeed = 5; // move speed of our camera
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), Time.deltaTime * moveSpeed);
    }
}
