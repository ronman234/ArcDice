using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    public GameObject[] characterSelection;
    public float maxPlayerHealth;
    [SerializeField]
    private float playerHealth;
    public int playerLevel;

    private PlayerController playerController;
    private Collider collider;

    private void Awake()
    {
        int character = PlayerPrefs.GetInt("CharacterSelected");
        characterSelection[character].SetActive(true);
        playerHealth = maxPlayerHealth;
        playerController = GetComponent<PlayerController>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(playerHealth <= 0)
        {
            OnDeath();
            return;
        }
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
    }

    private void OnDeath()
    {
        playerController.DoDeath();
        collider.enabled = false;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        Physics.IgnoreLayerCollision(6, 7);
        playerController.enabled = false;
        //TODO Reset
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }
        else
        {
            return;
        }
    }
}
