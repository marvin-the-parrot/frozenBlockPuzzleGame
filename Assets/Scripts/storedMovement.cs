using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storedMovement : MonoBehaviour
{
    public Vector3 momentum;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void unfreeze() {
        Debug.Log("Unfreezing");
        rb.velocity = momentum;
    }
}
