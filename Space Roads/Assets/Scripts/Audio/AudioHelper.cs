using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public void PlayClip(string audioClip)
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(audioClip);
        }
    }

    public void StopClip(string audioClip)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Stop(audioClip);
        }
    }
}
