using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayScript : MonoBehaviour
{
    public AudioSource buttonSounds;
    public AudioClip hoverClip;
    public AudioClip clickClip;
    // Start is called before the first frame update
    
    public void HoverSound()
    {
        buttonSounds.PlayOneShot(hoverClip);
    }

    public void ClickSound()
    {
        buttonSounds.PlayOneShot(clickClip);
    }
}
