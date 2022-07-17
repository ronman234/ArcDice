using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float maxPlayerHealth;
    [SerializeField]
    private float playerHealth;
    public int playerLevel;

    private PlayerController playerController;
    private Collider collider;

    private void Awake()
    {
        playerHealth = maxPlayerHealth;
        playerController = GetComponent<PlayerController>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(playerHealth <= 0)
        {
            OnDeath();
        }
    }

    private void TakeDamage(float damage)
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
