using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public AudioClip musicClipOne;
    public AudioClip soundClipOne;
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*musicSource.clip = soundClipOne;
    musicSource.loop = false;
    musicSource.Play();*/
}
