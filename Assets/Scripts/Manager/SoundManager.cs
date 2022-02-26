using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void SetVolume(string volumeName, float value)
    {
        if (value == -40f)
        {
            value = -80;    // mute
        }
        audioMixer.SetFloat(volumeName, value);
    }
}
