using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VOlumeControl : MonoBehaviour
{
    public AudioSource audioManager;
    public Scrollbar volume;

    public void UpdateVolume()
    {
        audioManager.volume = volume.value;
    }


}
