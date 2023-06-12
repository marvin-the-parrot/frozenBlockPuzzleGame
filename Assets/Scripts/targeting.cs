using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targeting : MonoBehaviour
{

    private GameObject outlinedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rayLength = 1000f;

        RaycastHit hit;
        Camera playerCamera = Camera.main;
        Ray pointerRay = playerCamera.ScreenPointToRay(new Vector3((playerCamera.pixelWidth-1)/2,(playerCamera.pixelHeight-1)/2,0f));
        Debug.DrawRay(pointerRay.origin, pointerRay.direction * rayLength, Color.red);

        if (Physics.Raycast(pointerRay, out hit, rayLength)) {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("interactable")) {
                if (hitObject != outlinedObject) {
                    // new object
                    if (outlinedObject != null) {
                        // remove old outline if it exists
                        ExecuteFunctionOnGameObject(outlinedObject, "removeOutline");
                    } 
                    // add new outline
                    outlinedObject = hitObject;
                    ExecuteFunctionOnGameObject(hitObject, "outline");  
                }

                if (Input.GetButtonDown("Fire1")) {
                    ExecuteFunctionOnGameObject(hitObject, "unfreeze");
                }
            } else {
                if (outlinedObject != null) {
                    ExecuteFunctionOnGameObject(outlinedObject, "removeOutline");
                    outlinedObject = null;
                }

                Debug.Log("Did not Hit");
            }
        } 
    }

    public void ExecuteFunctionOnGameObject(GameObject targetObject,string functionName) {
        targetObject.SendMessage(functionName);
    }


}
