using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName="Biome")]
public class Biome : ScriptableObject
{
    public float noiseScale = 30f;

    [Range(0, 1)]
    public float persistance = 0.16f;
    public float lacunarity = 2f;

    public float meshHeightMultiplier = 10f;
    public AnimationCurve meshHeightCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public AudioClip sound;

    public TerrainType[] regions;
    public PropType[] props;

    private void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;
        for (int i = 0; i < props.Length; ++i)
            if (props[i].amount <= 0)
                props[i].amount = 1000000;
    }
}
