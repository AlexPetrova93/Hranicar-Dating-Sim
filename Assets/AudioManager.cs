using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip galleryBg;
    [SerializeField] private AudioClip streetBg;
    [SerializeField] private AudioClip pubBg;
    [SerializeField] private AudioClip menuBg;
    [SerializeField] private AudioClip noiseBg;

    [SerializeField] private AudioSource ambienceSource;

    [SerializeField] private AudioSource typingSource;
    [SerializeField] private AudioSource pressSource;
    [SerializeField] private AudioSource nextSource;

    public void SetAmbience(ESetting setting)
    {
        Debug.Log("setting sound to setting " + setting);

        switch (setting)
        {
            case ESetting.Gallery:
                ambienceSource.clip = galleryBg;
                break;
            case ESetting.Street:
                ambienceSource.clip = streetBg;
                break;
            case ESetting.Pub:
                ambienceSource.clip = pubBg;
                break;
            case ESetting.Noise:
                ambienceSource.clip = noiseBg;
                break;
            case ESetting.Menu:
                ambienceSource.clip = menuBg;
                break;
            default:
                ambienceSource.clip = null;
                break;
        }

        ambienceSource.Play();
    }

    public void LerpVolume(float from, float to, float t)
    {
        ambienceSource.volume = Mathf.Lerp(to, from, t);
    }

    public void PlayPress()
    {
        pressSource.Play();
    }

    public void PlayNext()
    {
        nextSource.Play();
    }

    public void PlayTyping()
    {
        typingSource.Play();
    }

    public void StopTyping()
    {
        typingSource.Stop();
    }

}
