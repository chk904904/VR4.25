using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headSoundController : MonoBehaviour
{
    public AudioSource headAudio;
    public AudioClip punchSound;
    public AudioClip warningSound;

    public void playSound(int temp)
    {
        if (temp == 0)
        {
            headAudio.PlayOneShot(punchSound);
        } else
        {
            headAudio.PlayOneShot(warningSound);
        }
    }

}
