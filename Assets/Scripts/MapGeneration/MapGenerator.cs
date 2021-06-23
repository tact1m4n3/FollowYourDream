using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}

[System.Serializable]
public struct PropType
{
    public string name;
    public float minHeight;
    public float maxHeight;
    public float chance;
    public int amount;
    public float noiseHeight;
    public float heightOffset;
    public GameObject prefab;
}

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, FalloffMap, Mesh }
    public DrawMode drawMode;

    public const int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;

    public int octaves;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public bool autoUpdate;

    public string biomeName;
    public Biome[] biomes;

    private Biome biome;

    private void Start()
    {
        seed = MapData.seed;
        biomeName = MapData.biomeName;

        GenerateMap();
    }

    public void GenerateMap()
    {
        biome = GetBiomeByName(biomeName);
        if (!biome)
            return;

        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, biome.noiseScale, octaves, biome.persistance, biome.lacunarity, offset);
        float[,] propsNoiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed + 1, biome.noiseScale, octaves, biome.persistance, biome.lacunarity, offset);
        float[,] falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; ++y)
        {
            for (int x = 0; x < mapChunkSize; ++x)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp(noiseMap[x, y] - falloffMap[x, y], 0f, 1f);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < biome.regions.Length; ++i)
                {
                    if (currentHeight <= biome.regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = biome.regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            display.UpdateMapMat(TextureGenerator.TexFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.UpdateMapMat(TextureGenerator.TexFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, biome.meshHeightMultiplier, biome.meshHeightCurve, levelOfDetail),
                             PropGenerator.GenerateProps(biome.props, seed, biome.meshHeightMultiplier, biome.meshHeightCurve, propsNoiseMap, noiseMap),
                             TextureGenerator.TexFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.UpdateMapMat(TextureGenerator.TexFromHeightMap(falloffMap));
        }
    }

    private void OnValidate()
    {
        if (octaves < 0)
            octaves = 0;
    }

    private Biome GetBiomeByName(string name)
    {
        foreach (Biome b in biomes)
            if (b.name == name)
                return b;
        return null;
    }
}
