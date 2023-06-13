using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MyUIController : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button ExitButton;
    public SliderInt VolumeSlider;
    public VisualElement root;
    [SerializeField] AudioMixer masterMixer;


    void Start() 
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.visible = false;

        VolumeSlider = root.Q<SliderInt>("VolumeSlider");
        SetVolume(PlayerPrefs.GetInt("SavedMasterVolume", 100));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CallMenu();
        }
            
    }

    void CallMenu()
    {
        root.visible = true;

        ResumeButton = root.Q<Button>("ResumeButton");
        RestartButton = root.Q<Button>("RestartButton");
        ExitButton = root.Q<Button>("ExitButton");
        
        ResumeButton.clicked += ResumeButtonPressed;
        RestartButton.clicked += RestartButtonPressed;
        ExitButton.clicked += ExitButtonPressed;
    }

    void ResumeButtonPressed()
    {
        root.visible = false;
    }

    void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ExitButtonPressed()
    {
        Application.Quit();
    }

    void SetVolume(float _value)
    {
        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20);
    }

     void SetVolumeFromSlider()
    {
        SetVolume(VolumeSlider.value);
    }

    void RefreshSlider(float _value) { 
        VolumeSlider = root.Q<SliderInt>("VolumeSlider");
        VolumeSlider.value = (int)_value;
    }
}
