using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIMenuController : MonoBehaviour
{
    public Button ResumeButton;
    public Button RestartButton;
    public Button ExitButton;
    public Slider VolumeSlider;
    public VisualElement root;
    [SerializeField] AudioMixer masterMixer;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.visible = false;


        VolumeSlider = root.Q<Slider>("VolumeSlider");
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 50));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CallMenu();
        }

        SetVolumeFromSlider();
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
        if (_value == 0)
        {
            masterMixer.SetFloat("MasterVolume", 0.001f);
        }
        else
        {
            masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20);
        }
        
    }

     void SetVolumeFromSlider()
    {
        SetVolume(VolumeSlider.value);
    }

    void RefreshSlider(float _value) 
    { 
        VolumeSlider = root.Q<Slider>("VolumeSlider");
        VolumeSlider.value = _value;
    }
}
