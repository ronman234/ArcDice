using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement Settings
    public float moveSpeed = 5f;

    private PlayerControls playerInput;
    private Rigidbody rigidBody;

    //private float horizontalMovement;
    //private float verticalMovement;
    private Vector3 movement;
    private Vector3 velocity;
    private Animator animator;
    [SerializeField]
    private string attackType = "Bolt";

    [SerializeField]
    private GameObject pauseMenu;

    private void Awake()
    {

        rigidBody = GetComponentInChildren<Rigidbody>();
        playerInput = new PlayerControls();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Default.Attack.started += DoAttack;
        playerInput.Default.Heal.started += DoHeal;
        playerInput.Default.Dash.started += DoDash;
        playerInput.Default.Pause.started += DoPause;
    }
    private void OnDisable()
    {
        playerInput.Disable();
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
        animator.SetTrigger(attackType);
    }
    private void DoDash(InputAction.CallbackContext obj)
    {
        
        animator.SetTrigger("Dash");
    }
    private void DoHeal(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Heal");
    }
    private void DoPause(InputAction.CallbackContext obj)
    {
        pauseMenu.SetActive(true);
    }

    public void DoDeath()
    {
        animator.SetTrigger("Death");
    }
    
    public void UpdateAttack(string AttackName)
    {
        attackType = AttackName;
    }

}
