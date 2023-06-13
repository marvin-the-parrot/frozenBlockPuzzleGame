using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MyUIController : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button ExitButton;
    public Slider VolumeSlider;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        ResumeButton = root.Q<Button>("ResumeButton");
        RestartButton = root.Q<Button>("RestartButton");
        ExitButton = root.Q<Button>("ExitButton");
        VolumeSlider = root.Q<Slider>("VolumeSlider");

        ResumeButton.clicked += ResumeButtonPressed;
        RestartButton.clicked += RestartButtonPressed;
        ExitButton.clicked += ExitButtonPressed;
    }

    void ResumeButtonPressed()
    {
        SceneManager.LoadScene("Forrest");
    }

    void RestartButtonPressed()
    {
        SceneManager.LoadScene("TestingScene");
    }

    void ExitButtonPressed()
    {
        Application.Quit();
    }
}
