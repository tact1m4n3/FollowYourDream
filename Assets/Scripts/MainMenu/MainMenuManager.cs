using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private MapGenerator mapGenerator;
    bool settingsOn = false;
    public GameObject SettingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        SettingsPanel.SetActive(false);

        mapGenerator = GetComponent<MapGenerator>();

        mapGenerator.GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SceneManager.LoadScene("MapSettings");
    }
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
