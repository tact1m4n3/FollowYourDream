using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private MapGenerator mapGenerator;
    bool settingsOn = false;
    public GameObject SettingsPanel;
    public Slider sensitivitySlider;

    void Start()
    {
        SettingsPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ToggleFog(bool is_fog)
    {
        RenderSettings.fog = is_fog;
        Debug.Log("ToggledFog: " + is_fog);
    }

    public void ToggleFullscreen(bool is_fullscreen)
    {
        Screen.fullScreen = is_fullscreen;
        Debug.Log("ToggledFulscreen: " + is_fullscreen);
    }

    //Main Menu Stuff
    public void Play()
    {
        SceneManager.LoadScene("MapSettings");
    }
    
    public void Settings()
    {
        if(settingsOn == false)
        {
            settingsOn = true;
            SettingsPanel.SetActive(true);
        }
        else
        {
            settingsOn = false;
            SettingsPanel.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
