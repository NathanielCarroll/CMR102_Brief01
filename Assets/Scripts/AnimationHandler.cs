using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator; // a reference to our animator component

    public enum AnimationState { Idle,Walking,Running,Passing,Waving} // the different states of animation
    public AnimationState currentAnimationState; // the current state the animator is in



    /// <summary>
    /// handles updating the animation
    /// </summary>
    public AnimationState CurrentState
    {
        get
        {
            return currentAnimationState;
        }
        set

        {
            currentAnimationState = value; // set our aniamtion state to the value
            if (animator != null)
            {
                UpdateAnimator();
            }
            else
            {
                Debug.LogError("No animator has been assigned");
            }
        }
    }
        

        

    /// <summary>
    /// Update our animator to match the current state of our character
    /// </summary>
    private void UpdateAnimator()
    {
        switch(currentAnimationState)
        {
            case AnimationState.Idle:
                {
                    // reset our animator back to idle
                    ResetToIdle();
                    break;
                }
            case AnimationState.Passing:
                {
                    animator.SetBool("Passing", true);
                    ResetToIdle();
                        break;;
                }
            case AnimationState.Running:
                {
                    ResetToIdle();
                    // Set our animater to the running animation
                    animator.SetBool("Running", true);
                    break;
                }
            case AnimationState.Walking:
                {
                    ResetToIdle();
                    // set our animator to the walking animation
                    animator.SetBool("Walking", true);
                    break;

                }
            case AnimationState.Waving:
                {
                    ResetToIdle();
                    // set our animator to the waving state
                    animator.SetBool("Wave", true);
                    break;
                }
        }
    }



    /// <summary>
    /// Resets our animator to idle state
    /// </summary>
    private void ResetToIdle()
    {
        animator.SetBool("Passing", false);
        animator.SetBool("Running", false);
        animator.SetBool("Wave", false);
        animator.SetBool("Walking", false);
    }
    
}
