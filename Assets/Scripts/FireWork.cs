using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : MonoBehaviour
{

    public AudioClip fireWorkSound; // the fire work sound
    public AudioSource audioSource; // refernce to audio source
    public int numberOfFireworks = 3; // the number of fireworks that will be spawned
    public float initialDelay = 2; // am initial delay before the first firework is spawned
    public float timeBetweenFireWorks = 0.5f; // half a second between each firework
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayFireworks()); // start our coroutine up here
    }


    /// <summary>
    /// a coroutine that allows us to dictate when certain parts of code should be played
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayFireworks()
    {
        yield return new WaitForSeconds(initialDelay); // wait a couple of seconds before continuing with our code
        for(int i =0; i<numberOfFireworks; i++)
        {
            audioSource.PlayOneShot(fireWorkSound); // play our firework sound
            yield return new WaitForSeconds(timeBetweenFireWorks); // now wait before we iterate to the next part of the loop
        }
        
        yield return null;
    }
}
