using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storedMovement : MonoBehaviour
{
    public Vector3 momentum;

    private float indicatorDistance = 8;
    private GameObject momentumIndicatorPrefab;
    private GameObject momentumIndicator;

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
        momentumIndicatorPrefab = Resources.Load("Models/arrow", typeof(GameObject)) as GameObject;

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

    private void showMomentumIndicator() {
        if (momentum==Vector3.zero) {
            return;
        }

        if (momentumIndicator!=null) {
            return;
        }
        Vector3 position = gameObject.transform.position;

        position.x = momentum.normalized.x;
        position.y = momentum.normalized.y;
        position.z = momentum.normalized.z;

        position *= indicatorDistance;
        position = gameObject.transform.position + position;
        momentumIndicator = Instantiate(momentumIndicatorPrefab, position, Quaternion.LookRotation(momentum.normalized),gameObject.transform);
    }

    private void destroyMomentumIndicator() {
        Destroy(momentumIndicator);
    }

    public void freeze() {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        objRenderer.material = frozenMaterial;
    }

    public void unfreeze() {
        destroyMomentumIndicator();
        rb.isKinematic = false;
        rb.velocity = momentum;
        objRenderer.material = movingMaterial;

    }

    public void outline() {
        outlineObject.enabled = true;
        showMomentumIndicator();
    }

    public void removeOutline() {
        if (outlineObject != null) {
            outlineObject.enabled = false;
        }
        destroyMomentumIndicator();
    }
}

    
