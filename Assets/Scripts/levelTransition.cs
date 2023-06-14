using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTransition : MonoBehaviour
{
    public string nextLevelName = "TestingScene"; // name of next level

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Character")) {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
