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

    private void Awake()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
        playerInput = new PlayerControls();
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        float horizontalMovement = playerInput.Default.HorizontalMove.ReadValue<float>();
        float verticalMovement = playerInput.Default.VerticalMove.ReadValue<float>();
        Debug.Log("Hor: " + horizontalMovement + " Vert: " + verticalMovement);
        movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        Vector3 currentPosition = rigidBody.transform.position;
        velocity = movement * moveSpeed;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = velocity;
        

        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        Quaternion currentRot = rigidBody.transform.rotation;

        rigidBody.transform.rotation = Quaternion.Lerp(currentRot, Quaternion.Euler(new Vector3(0f, -angle - 90, 0f)), 0.5f);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
