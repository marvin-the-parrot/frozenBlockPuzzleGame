using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour {

    public int playerNumber;
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

    public InputActionAsset actions;
    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction lookAction;

    private void Awake() {
        rb = GetComponent<Rigidbody>();

        if (playerNumber==1) {
            actionMap = actions.FindActionMap("Player 1");
        } else if(playerNumber == 2) {
            actionMap = actions.FindActionMap("Player 2");
        }

        moveAction = actionMap.FindAction("Move");
        lookAction = actionMap.FindAction("Look");
        actionMap.FindAction("Jump").performed += OnJump;
        
    }

    public void OnJump(InputAction.CallbackContext context) {
         Jump();
    }

    private void Update() {
        

    }


    private void Jump() {
        if (!isJumping) {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("interactable") || collision.gameObject.CompareTag("Wall")) {
            isJumping = false;
        }
    }

    private void FixedUpdate() {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        Vector2 look = lookAction.ReadValue<Vector2>();
        Debug.Log(look);
        lookX = look.x;
        lookY = look.y;

        movementInput = new Vector3(movement.x, 0f, movement.y);

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
    void OnEnable() {
        actionMap.Enable();
    }
    void OnDisable() {
        actionMap.Disable();
    }
}
