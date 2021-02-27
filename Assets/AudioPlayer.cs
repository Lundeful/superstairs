using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip jumpClip;
    public AudioClip pointClip;
    public AudioClip gameOverClip;
    public AudioClip hoverClip;
    public AudioClip clickClip;
    // Start is called before the first frame update

    public void jumpSound()
    {
        source.PlayOneShot(jumpClip);
    }

    public void pointSound()
    {
        source.PlayOneShot(pointClip);
    }

    public void gameOverSound()
    {
        source.PlayOneShot(gameOverClip);
    }

    public void hoverSound()
    {
        source.PlayOneShot(hoverClip);
    }

    public void clickSound()
    {
        source.PlayOneShot(clickClip);
    }
}
