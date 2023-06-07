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
            if (hit.collider.gameObject != outlinedObject) {
                if (outlinedObject != null) {
                    Outline outlinedObjectOutline = outlinedObject.GetComponent<Outline>();
                    if (outlinedObjectOutline != null) {
                        outlinedObjectOutline.enabled = false;
                    }
                }

                GameObject hitObject = hit.collider.gameObject;
                if (hitObject != null) {
                    Outline hitObjectOutline = hitObject.GetComponent<Outline>();
                    if (hitObjectOutline != null) {
                        hitObjectOutline.enabled = true;
                    }
                    outlinedObject = hitObject;
                    ExecuteFunctionOnGameObject(hitObject, "outline");
                }
            }

            if (Input.GetButtonDown("Fire1")) {
                ExecuteFunctionOnGameObject(hit.collider.gameObject, "unfreeze");
            }
        } else {
            Debug.Log("Did not Hit");
        }
    }

    public void ExecuteFunctionOnGameObject(GameObject targetObject,string functionName) {
        targetObject.SendMessage(functionName);
    }


}
