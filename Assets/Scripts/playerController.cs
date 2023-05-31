using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour {
    public float moveSpeed = 5f;        // Player movement speed
    public float rotationSpeed = 10f;   // Player rotation speed

    private Rigidbody rb;
    private Vector3 movementInput;
    private float turnInput;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        // Read input
        float moveZ = Input.GetAxis("Vertical");
        float moveY = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");

        // Calculate movement input
        movementInput = new Vector3(moveY,0f,moveZ);

        turnInput = mouseX;
    }

    private void FixedUpdate() {
        // Move the player
        MovePlayer();

        // Turn the player
        TurnPlayer();
    }

    private void MovePlayer() {
        // Calculate movement velocity
        Vector3 moveVelocity = transform.TransformDirection(movementInput) * moveSpeed;

        // Apply movement velocity to the rigidbody
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    private void TurnPlayer() {
        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(0f, turnInput * rotationSpeed, 0f);

        // Apply rotation to the rigidbody
        rb.MoveRotation(rb.rotation * rotation);
    }
}
