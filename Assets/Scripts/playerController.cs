using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour {
    public float moveSpeed = 5f;        // Player movement speed
    public Vector2 sensitivity = new Vector2(10f,2f);


    public float gravityScale = 2f; // Controls the falling speed

    public float jumpForce = 5f;
    private bool isJumping = false;

    public float speed = 1f;

    public GameObject followTransform;
    private Rigidbody rb;
    private Vector3 movementInput;
    private float lookX;
    private float lookY;


    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {

        // Read input
        float moveZ = Input.GetAxis("Vertical");
        float moveY = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Jump") && !isJumping) {
            Jump();
        }

        // Calculate movement input
        movementInput = new Vector3(moveY, 0f, moveZ);

        lookX = mouseX;
        lookY = mouseY;
    }

    private void Jump() {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("interactable") || collision.gameObject.CompareTag("Wall")) {
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
        Quaternion rotation = Quaternion.Euler(0f, lookX * sensitivity.x, 0f);
        Quaternion yaw = Quaternion.Euler(-lookY * sensitivity.y, 0f, 0f);

        gameObject.transform.rotation *= rotation;
        followTransform.transform.rotation *= yaw;

    }
}
