using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{

    public static Texture2D TexFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;

        tex.SetPixels(colorMap);
        tex.Apply();

        return tex;
    }

    public static Texture2D TexFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return TexFromColorMap(colorMap, width, height);
    }

}
