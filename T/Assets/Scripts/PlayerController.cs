using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float playerMoveSpeed = 5f;

    float verticalInput;
    float horizontalInput;
    float movementMultiplier = 5f;


    Rigidbody playerRb;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void InitializeComponents()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void HandleInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

    }

    private void MovePlayer()
    {
        playerRb.AddForce(playerRb.transform.InverseTransformDirection(moveDirection.normalized) * playerMoveSpeed * movementMultiplier, ForceMode.Force);
        
    }
}
