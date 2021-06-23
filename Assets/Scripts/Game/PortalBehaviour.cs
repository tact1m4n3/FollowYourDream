using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour
{
    private int seed;
    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(0, 1000000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MapData.seed = seed;

            SceneManager.LoadScene("Game");
        }
    }
}
