using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VOlumeControl : MonoBehaviour
{
    public AudioSource audioManager;
    public Scrollbar volume;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }
    public void UpdateVolume()
    {
        audioManager.volume = volume.value;
    }


}
