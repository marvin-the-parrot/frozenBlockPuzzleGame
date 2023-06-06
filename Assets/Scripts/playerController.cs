using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour {
    public float moveSpeed = 5f;        // Player movement speed
    public float rotationSpeed = 10f;   // Player rotation speed

    public float gravityScale = 2f; // Controls the falling speed

    public float jumpForce = 5f; // The force applied to make the player jump
    private bool isJumping = false; // Flag to check if the player is jumping

    private Rigidbody rb;
    private Vector3 movementInput;
    private float turnInput;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        // Read input
        float moveZ = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        if (Input.GetButtonDown("Jump") && !isJumping) {
            Jump();
        }

        // Calculate movement input
        movementInput = new Vector3(0f,0f,moveZ);

        turnInput = mouseX;
    }

    private void Jump() {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isJumping = false;
        }
    }

    private void FixedUpdate() {
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
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
