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

    public virtual void OnEnable()
    {
        SetUpAgentFromConfiguration();
        Agent.enabled = true;
        Animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        collision = GetComponent<Collider>();
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

        Health = EnemyScriptableObject.Health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            TakeDamage(2);
        else
            return;
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
        
        Animator.SetTrigger("Death");
        
        Destroy(gameObject, 4.0f);
        this.enabled = false;
    }
}
