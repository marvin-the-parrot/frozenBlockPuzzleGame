using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targeting : MonoBehaviour
{
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

        if (Input.GetButtonDown("Fire1")) {
            if (Physics.Raycast(pointerRay, out hit, 1000 ))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log("Did Hit");
                    ExecuteFunctionOnGameObject(hit.collider.gameObject,"unfreeze");
            } else {
                    Debug.Log("Did not Hit");
            }
        }
    }

    public void ExecuteFunctionOnGameObject(GameObject targetObject,string functionName) {
        targetObject.SendMessage(functionName);
    }


}
