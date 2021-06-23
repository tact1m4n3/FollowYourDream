using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PropData
{
    public Vector3 pos;
    public GameObject prefab;
}

public static class PropGenerator
{
    public static float propChancePerUnit = 0.1f;

    public static PropData[] GenerateProps(PropType[] propTypes, int seed, float heightMultiplier, AnimationCurve animationCurve, float[,] propsNoiseMap, float[,] noiseMap)
    {
        Random.InitState(seed);

        int mapWidth = noiseMap.GetLength(0);
        int mapHeight = noiseMap.GetLength(1);
        PropData[] props = new PropData[mapWidth * mapHeight];

        float topLeftX = (mapWidth - 1) / -2f;
        float topLeftZ = (mapHeight - 1) / 2f;

        for (int i = 0; i < propTypes.Length; ++i)
        {
            propTypes[i].noiseHeight = Mathf.Round(Random.Range(0.5f, 0.9f) * 100f) * 0.01f;
        }

        int[] amounts = new int[propTypes.Length];
        for (int i = 0; i < amounts.Length; ++i)
        {
            amounts[i] = propTypes[i].amount;
        }

        for (int y = 0; y < mapHeight; y += 1)
        {
            for (int x = 0; x < mapWidth; x += 1)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < propTypes.Length; ++i)
                {
                    if (amounts[i] > 0 && RollDice(propTypes[i].chance) && currentHeight >= propTypes[i].minHeight && currentHeight <= propTypes[i].maxHeight && propsNoiseMap[x, y] <= propTypes[i].noiseHeight)
                    {
                        props[y * mapWidth + x].pos = new Vector3(topLeftX + x, animationCurve.Evaluate(currentHeight) * heightMultiplier + propTypes[i].heightOffset, topLeftZ - y);
                        props[y * mapWidth + x].prefab = propTypes[i].prefab;
                        --amounts[i];
                        break;
                    }
                }
            }
        }

        return props;
    }

    private static bool RollDice(float chance)
    {
        float random = Random.Range(0f, 100f);
        if (random <= chance)
            return true;
        return false;
    }
}
