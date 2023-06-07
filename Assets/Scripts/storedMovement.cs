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

    // Update is called once per frame
    void Update()
    {

        
    }

    public void unfreeze() {

        rb.velocity = momentum;
    }

    public void outline() {
        
        if (outlineObject == null) {
            return;
        }
        outlineObject.enabled = true;
        
    }
}

    
