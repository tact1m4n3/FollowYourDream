using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private MapGenerator mapGenerator;
    bool settingsOn = false;
    public GameObject SettingsPanel;

    void Start()
    {
        SettingsPanel.SetActive(false);
    }

    void Update()
    {

    }

    public void ToggleFullscreen(bool is_fullscreen)
    {
        Screen.fullScreen = is_fullscreen;
        
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
