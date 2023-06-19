using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class targeting : MonoBehaviour
{
    public Camera playerCamera;
    private GameObject outlinedObject;
    private float playerNumber;

    private InputActionAsset actions;
    private InputActionMap actionMap;
    private bool interactionPerformed;
    // Start is called before the first frame update
    void Awake()
    {
        playerNumber = GetComponent<playerController>().playerNumber;
        actions = GetComponent<playerController>().actions;

        if (playerNumber == 1) {
            actionMap = actions.FindActionMap("Player 1");
        } else if (playerNumber == 2) {
            actionMap = actions.FindActionMap("Player 2");
        }

        actionMap.FindAction("Interact").performed += ctx => interactionPerformed = true;
    }

    // Update is called once per frame
    void Update()
    {
        float rayLength = 1000f;
        Vector3 offset = Vector3.zero;
        if (gameObject.name == "Player 2") {
            offset = new Vector3(playerCamera.pixelWidth, 0f, 0f);
        }

        RaycastHit hit;
        Ray pointerRay = playerCamera.ScreenPointToRay(new Vector3(((playerCamera.pixelWidth-1) / 2) + offset.x, ((playerCamera.pixelHeight-1)/ 2) + offset.y, 0f));
        Debug.DrawRay(pointerRay.origin, pointerRay.direction * rayLength, Color.red);
        Debug.Log(gameObject.name + " H" + playerCamera.pixelWidth + " W" + playerCamera.pixelHeight);

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

                if (interactionPerformed) {
                    ExecuteFunctionOnGameObject(hitObject, "unfreeze");
                    interactionPerformed = false;
                }
            } else {
                if (outlinedObject != null) {
                    ExecuteFunctionOnGameObject(outlinedObject, "removeOutline");
                    outlinedObject = null;
                }

            }
        } 
    }

    void OnEnable() {
        actionMap.Enable();
    }
    void OnDisable() {
        actionMap.Disable();
    }

    public void ExecuteFunctionOnGameObject(GameObject targetObject,string functionName) {
        targetObject.SendMessage(functionName);
    }


}
