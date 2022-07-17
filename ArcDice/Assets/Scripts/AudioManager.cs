using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip bossMusic;
    public AudioClip menuMusic;

    private void Awake()
    {
        //hatch = GameObject.FindGameObjectWithTag("Hatch").GetComponent<Hatch>();
        //hatch.enabled = false;
        int instancesInScene = FindObjectsOfType<AudioManager>().Length;

        if (instancesInScene > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        source = GetComponent<AudioSource>();

        PlayAudio(menuMusic);
    }

    public void PlayAudio(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.loop = true;
        source.Play();
    }
}
