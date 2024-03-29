using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool tgm;

    [SerializeField]
    private GameObject pauseMenu;

    public GameObject[] characterSelection;
    public float maxPlayerHealth;
    [SerializeField]
    private float playerHealth;
    public float Health
    {
        get { return playerHealth; }
        set { }
    }
    public int playerLevel;

    private PlayerController playerController;
    private Collider collider;

    public HUDManager hud;

    private void Awake()
    {
        int character = PlayerPrefs.GetInt("CharacterSelected");
        characterSelection[character].SetActive(true);
        playerHealth = maxPlayerHealth;
        playerController = GetComponent<PlayerController>();
        collider = GetComponent<Collider>();
        
    }
    private void Start()
    {
        GameManager.Instance.playerManager = this;
    }
    private void Update()
    {
        if(playerHealth <= 0) 
        {
            OnDeath();
            return;
        }

        if (gameObject.transform.position.y <= -10f) //Check if player fell out of the map
        {
            OnDeath();
            return;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!tgm)
        {
            playerHealth -= damage;
            hud.UpdateHealth();
        }
    }

    public void OnDeath()
    {
        if (!tgm)
        {
            playerController.DoDeath();
            collider.enabled = false;
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            Physics.IgnoreLayerCollision(6, 7);
            playerController.enabled = false;
            //TODO Reset
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        TakeDamage(1);
    //        playerController.DoDamage();
    //    }
    //}
}
