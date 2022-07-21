using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public static GameManager gameManager;
    public static AudioManager audioManager;

    private void Awake()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //gameManager = FindObjectOfType<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        GameManager.Instance.hatch = this;
        gameObject.GetComponent<Collider>().enabled = false;

        audioManager.PlayAudio(audioManager.menuMusic);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            //gameManager.UpdatePlayerLevel();
            GameManager.Instance.LoadScene(GameManager.generatorLevel);
        }
    }
}
