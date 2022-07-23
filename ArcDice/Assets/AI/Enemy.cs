using UnityEngine.AI;
using UnityEngine;

public class Enemy : PoolableObject
{
    public NavMeshAgent Agent;
    public EnemyScriptableObject EnemyScriptableObject;
    public float Health = 100;
    public EnemyMovement enemyMovement;
    public Animator Animator;
    public Collider collision;
    public Rigidbody rb;
    public bool isBoss;
    private bool isDead;
    private GameObject Player;
    public GameManager gameManager;
    private AudioManager audioManager;
    private PlayerManager playerManager;

    public virtual void OnEnable()
    {
        SetUpAgentFromConfiguration();
        Agent.enabled = true;
        Animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        collision = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        Player = enemyMovement.Player.gameObject;
        
        
        //Debug.Log(gameManager);
    }

    private void Awake()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (isBoss)
        {
            audioManager.PlayAudio(audioManager.bossMusic);
        }
        
    }

    public override void OnDisable()
    {
        base.OnDisable();

        Agent.enabled = false;
    }

    public virtual void SetUpAgentFromConfiguration()
    {
        Agent.acceleration = EnemyScriptableObject.Acceleration;
        Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;  
        Agent.areaMask = EnemyScriptableObject.AreaMask;
        Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
        Agent.baseOffset = EnemyScriptableObject.BaseOffset;
        Agent.height = EnemyScriptableObject.Height;
        Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
        Agent.radius = EnemyScriptableObject.radius;
        Agent.speed = EnemyScriptableObject.Speed;
        Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;
        isBoss = EnemyScriptableObject.Boss;

        
        Health = EnemyScriptableObject.Health;
        if (isBoss)
            Health = Health * GameManager.Instance.currentPlayerLevel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Animator.SetTrigger("Attack");

            if (Health > 1)
            {
                if (isBoss)
                {
                    playerManager.TakeDamage(2 + (gameManager.currentPlayerLevel * 2));
                    Debug.Log("Player took " + (1 + 2 + (gameManager.currentPlayerLevel * 2)) + " damage.");
                }
                else
                {
                    playerManager.TakeDamage(1 + (gameManager.currentPlayerLevel / 2));
                    Debug.Log("Player took " + (1 + (gameManager.currentPlayerLevel / 2)) + " damage.");
                }
            }
        }

        
    }

    private void Update()
    {
        if(Health <= 0)
        {
            //TODO Die
            OnDeath();
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    private void OnDeath()
    {
        if (!isDead)
        {
            isDead = true;
            if (isBoss)
            {
                Animator.SetTrigger("Death");
                rb.freezeRotation = true;
                GameManager.Instance.HatchOn();
                Destroy(gameObject, 4.0f);
                this.enabled = false;

                //Player.GetComponent<PlayerManager>().playerLevel++;
                //Debug.Log(GetComponent<PlayerManager>().playerLevel);
                return;
            }

            Animator.SetTrigger("Death");
            rb.freezeRotation = true;
            Destroy(gameObject, 4.0f);
            this.enabled = false;
        }
    }

}
