using UnityEngine;
using System.Collections;

public class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        if (scale <= 0) scale = 0.001f;

        float[,] noiseMap = new float[mapWidth, mapHeight];

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; y < mapWidth; x++) {
                float sampleX = x / scale; 
                float sampleY = y / scale; 

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
