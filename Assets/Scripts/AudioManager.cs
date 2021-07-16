using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip roamingMusic; // the music we will use when roaming
    public AudioClip playingMusic; // the music we will use whilst we play
    public AudioClip fleeingMusic; // play a people screaming sound

    private AudioClip currentTrack; // the current track being played
    private AudioClip previousTrack; // the previous track that was played
    public AudioSource audioSource; // a reference to our audio source where music will be played from


    /// <summary>
    /// summary this gets called every time the script is turned on and off
    /// </summary>
    private void OnEnable()
    {
        if(currentTrack == null)
        {
            currentTrack = roamingMusic; // just music to default
        }
        ChangeTrack(currentTrack); // start playing music when we enter game
    }



    /// <summary>
    /// Plays the roaming music
    /// </summary>
    public void PlayRoamingMusic()
    {
        currentTrack = roamingMusic;
        ChangeTrack(currentTrack);
    }


    /// <summary>
    /// Plays the playing music
    /// </summary>
    public void PlayPlayingMusic()
    {
        currentTrack = playingMusic;
        ChangeTrack(currentTrack);
    }


    /// <summary>
    /// plays the fleeing music
    /// </summary>
    public void PlayFleeingMusic ()
    {
        currentTrack = fleeingMusic;
        ChangeTrack(currentTrack);
    }


    /// <summary>
    /// play the previous track
    /// </summary>
    public void PlayPreviousTrack()
    {
        if (previousTrack == null)
        {
            return;
        }
        currentTrack = previousTrack; // set the current track to the previous track
        ChangeTrack(currentTrack); // play our previous track
    }


    /// <summary>
    /// this function changes the clip bein played at the moment
    /// </summary>
    /// <param name="clip"><</param>
    private void ChangeTrack(AudioClip clip)
    {
        audioSource.Stop(); // stop playing audio
        if(audioSource.clip != clip) // if the clip in the audio source is not equal to what we are tying to play
        {
            previousTrack = audioSource.clip; // store the previous track
            audioSource.clip = clip; // set the new track
        }
        audioSource.loop = true; // set the music to loop
        audioSource.Play(); // start playing music
    }
}

