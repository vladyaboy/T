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

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void MovePlayer()
    {
        playerRb.AddForce(playerRb.transform.InverseTransformDirection(moveDirection.normalized) * playerMoveSpeed * movementMultiplier, ForceMode.Force);
        
    }
}
