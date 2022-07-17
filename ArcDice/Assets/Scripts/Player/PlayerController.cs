using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement Settings
    public float moveSpeed = 5f;

    public List<SpellShape> spellShapes;
    public List<SpellElement> spellElements;
    public List<Material> dieFaces;
    public GameObject die;
    public float rollTime;

    private PlayerControls playerInput;
    private Rigidbody rigidBody;

    //private float horizontalMovement;
    //private float verticalMovement;
    private Vector3 movement;
    private Vector3 velocity;
    private Animator animator;
    public string AttackTrigger;
    private bool canAttack;
    [NonSerialized] public UnityEvent afterRoll;


    private void Awake()
    {
        afterRoll = new UnityEvent();
        canAttack = false;
        rigidBody = GetComponentInChildren<Rigidbody>();
        playerInput = new PlayerControls();
        animator = GetComponentInChildren<Animator>();

        //Create Spell
        Spell spell = gameObject.AddComponent<Spell>();
        spell.element = SpellCreator.RollElementType(spellElements);
        spell.shape = SpellCreator.RollAttackType(spellShapes);
        spell.playerController = this;
        spell.playerManager = GetComponent<PlayerManager>();

        //Create Die
        GameObject dieSpawned = Instantiate(die, Vector3.up * 3, Quaternion.Euler(0, -90, 0));
        FollowCam follow = dieSpawned.AddComponent<FollowCam>();
        follow.followTarget = this;
        follow.yaw = 0;
        follow.followDistanceZ = 0;
        follow.followDistanceY = 3f;
        List<Transform> children = new List<Transform>(dieSpawned.GetComponentsInChildren<Transform>());
        children.RemoveAt(0);
        List<Material> availableFaces = new List<Material>(dieFaces);
        Transform childToRemove = null;
        foreach (Transform child in children)
        {
            if (child.name == "Side_3_low")
            {
                child.GetComponent<MeshRenderer>().materials = new Material[1] { availableFaces[spellElements.IndexOf(spell.element)] };
                availableFaces.RemoveAt(spellElements.IndexOf(spell.element));
                childToRemove = child;
            }
        }
        children.Remove(childToRemove);

        foreach (Transform child in children)
        {
            if (child.GetComponent<MeshRenderer>() == null)
            {
                continue;
            }
            int face = UnityEngine.Random.Range(0, availableFaces.Count);
            child.GetComponent<MeshRenderer>().materials = new Material[1] { availableFaces[face] };
            availableFaces.RemoveAt(face);
        }

        dieSpawned.GetComponent<Animator>().SetBool("D4", true);
        dieSpawned.GetComponent<Animator>().SetTrigger(spell.shape.name);
        afterRoll.AddListener(delegate { canAttack = true; });
        afterRoll.AddListener(delegate { Destroy(dieSpawned); });

        AttackTrigger = GetComponent<Spell>().shape.animationTriggerName;

        StartCoroutine(WaitForRoll());
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Default.Attack.started += DoAttack;
        playerInput.Default.Heal.started += DoHeal;
        playerInput.Default.Dash.started += DoDash;
    }
    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.Default.Attack.started += DoAttack;
        playerInput.Default.Heal.started += DoHeal;
        playerInput.Default.Dash.started += DoDash;
    }

    private void Update()
    {

        float horizontalMovement = playerInput.Default.HorizontalMove.ReadValue<float>();
        float verticalMovement = playerInput.Default.VerticalMove.ReadValue<float>();
//        Debug.Log("Hor: " + horizontalMovement + " Vert: " + verticalMovement);
        movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        Vector3 currentPosition = rigidBody.transform.position;
        velocity = movement * moveSpeed;


        bool isIdle = horizontalMovement == 0 && verticalMovement == 0;
        if (isIdle)
        {
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", 0);
        }
        else
        {
            animator.SetFloat("Vertical", playerInput.Default.VerticalMove.ReadValue<float>());
            animator.SetFloat("Horizontal", playerInput.Default.HorizontalMove.ReadValue<float>());
        }

        velocity.y = rigidBody.velocity.y;

    }

    private void FixedUpdate()
    {
        // Move player
        rigidBody.velocity = velocity;
        
        

        // Rotate towards mouse position
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        Quaternion currentRot = rigidBody.transform.rotation;
        rigidBody.transform.rotation = Quaternion.Lerp(currentRot, Quaternion.Euler(new Vector3(0f, -angle+180, 0f)), 0.5f);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        if (!canAttack)
        {
            return;
        }
        animator.SetTrigger(GetComponent<Spell>().shape.animationTriggerName);
        GetComponent<Spell>().CastSpell();
        //animator.SetTrigger(GetComponent<Spell>().shape.animationTriggerName);
        StartCoroutine(AttackCooldown());
    }
    private void DoDash(InputAction.CallbackContext obj)
    {
        
        animator.SetTrigger("Dash");
    }
    private void DoHeal(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Heal");
    }
    public void DoDeath()
    {
        animator.SetTrigger("Death");
    }

    public void DoDamage()
    {
        animator.SetTrigger("Damage");
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    private IEnumerator WaitForRoll()
    {
        yield return new WaitForSeconds(rollTime);
        afterRoll.Invoke();
    }
}
