using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSettingsManager : MonoBehaviour
{
    public InputField seedInput;
    public Dropdown biomeDropdown;

    private MapGenerator mapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();

        UpdateMapData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMapData()
    {
        MapData.seed = int.Parse(seedInput.text);
        MapData.biomeName = biomeDropdown.options[biomeDropdown.value].text;

        UpdateMapGeneratorValues();

        mapGenerator.GenerateMap();
    }

    public void Play()
    {
        UpdateMapData();

        SceneManager.LoadScene("Game");
    }

    private void UpdateMapGeneratorValues()
    {
        mapGenerator.seed = MapData.seed;
        mapGenerator.biomeName = MapData.biomeName;
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
