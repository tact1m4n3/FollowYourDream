using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private MapGenerator mapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();

        mapGenerator.GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SceneManager.LoadScene("MapSettings");
    }
}
