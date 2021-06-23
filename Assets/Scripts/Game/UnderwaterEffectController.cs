using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterEffectController : MonoBehaviour
{
    public bool enable = true;

    public float waterHeight;

    public Color fogColor = Color.blue;
    public float fogDensity = 0.075f;

    private bool defaultFog = false;
    private Color defaultFogColor = Color.black;
    private float defaultFogDensity = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
            UpdateFogSettings(transform.position.y <= waterHeight);

    }

    void UpdateFogSettings(bool underwater)
    {
        RenderSettings.fog = underwater ? true : defaultFog;
        RenderSettings.fogColor = underwater ? fogColor : defaultFogColor;
        RenderSettings.fogDensity = underwater ? fogDensity : defaultFogDensity;
    }
}
