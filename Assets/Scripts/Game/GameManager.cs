using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menu;

    [HideInInspector]
    public bool isMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.fog = IntersceneSettings.isFog;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenu)
            {
                isMenu = false;
                menu.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                isMenu = true;
                menu.SetActive(true);

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }


    public void GoToMainMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }

    public void GoToMapSettings()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        SceneManager.LoadScene("MapSettings");
    }
}
