using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource footstepSound;
    [SerializeField]
    private AudioSource attackSound;
    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private AudioSource randSound;

    public void PlayFootstepSound()
    {
        footstepSound.Play();
    }

    public void PlayattackSound()
    {
        attackSound.Play();
    }

    public void PlayjumpSound()
    {
        jumpSound.Play();
    }

    public void PlayrandSound()
    {
        randSound.Play();
    }
}
