using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storedMovement : MonoBehaviour
{
    public Vector3 momentum;
    private Rigidbody rb;
    private Outline outlineObject;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Create Outline and disable it until needed
        outlineObject = gameObject.AddComponent<Outline>();
        outlineObject.OutlineMode = Outline.Mode.OutlineAll;
        outlineObject.OutlineColor = Color.yellow;
        outlineObject.OutlineWidth = 5f;
        outlineObject.enabled = false;
    }

    private void OnCollisionEnter(Collision collision) {
        // Collision with other cube
        // TODO: transfere momentum to other cube maybe
        if (collision.gameObject.CompareTag("interactable")) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = true;
        }

        // Collision with wall, wall absorbs all momentum
        if (collision.gameObject.CompareTag("Wall")) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = true;
        }
    }

    public void unfreeze() {
        rb.isKinematic = false;
        rb.velocity = momentum;
    }

    public void outline() {
        
        outlineObject.enabled = true;
        
    }
}

    
