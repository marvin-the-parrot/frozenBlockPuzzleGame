using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storedMovement : MonoBehaviour
{
    public Vector3 momentum;

    private Material frozenMaterial;
    private Material movingMaterial;

    private Rigidbody rb;
    private Outline outlineObject;
    private Renderer objRenderer;

    // Start is called before the first frame update
    void Start()
    {
        frozenMaterial = Resources.Load("Materials/DebugFrozen", typeof(Material)) as Material;
        movingMaterial = Resources.Load("Materials/DebugMoving", typeof(Material)) as Material;
        objRenderer = gameObject.GetComponent<Renderer>();
        objRenderer.material = frozenMaterial;

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
            freeze();
        }

        // Collision with wall, wall absorbs all momentum
        if (collision.gameObject.CompareTag("Wall")) {
            freeze();
        }

        // Collision with player
        if (collision.gameObject.CompareTag("Character")) {
            freeze();
        }
    }

    public void freeze() {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        objRenderer.material = frozenMaterial;
    }

    public void unfreeze() {
        rb.isKinematic = false;
        rb.velocity = momentum;
        objRenderer.material = movingMaterial;

    }

    public void outline() {
        outlineObject.enabled = true; 
    }
}

    
