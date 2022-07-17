using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public static GameManager gameManager;
    public static AudioManager audioManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        gameManager.hatch = this;
        gameObject.GetComponent<Collider>().enabled = false;

        audioManager.PlayAudio(audioManager.menuMusic);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            //gameManager.UpdatePlayerLevel();
            gameManager.LoadScene(GameManager.generatorLevel);
        }
    }
}
