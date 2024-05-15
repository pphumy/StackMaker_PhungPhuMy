using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audiosrc;
    public AudioClip hitSfx, winSfx, swipeSfx;

    // Update is called once per frame
    public void PlayWinSfx()
    {
        audiosrc.PlayOneShot(winSfx);
    }
    public void PlayHitSfx()
    {
        
        audiosrc.PlayOneShot(hitSfx);
    }
    public void PlaySwipeSfx()
    {
        audiosrc.clip = swipeSfx;
        audiosrc.Play();
        //audiosrc.PlayOneShot(swipeSfx);
    }
}
