using System.Security;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap };
    [SerializeField] private DrawMode _drawMode;
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    [SerializeField] private int _seed;
    [SerializeField] private float _noiseScale;
    [SerializeField] private int _octaves;
    [SerializeField] private float _lucunarity;
    [SerializeField] private Vector2 _offset;
    [Range(0, 1)]
    [SerializeField] private float _persistence;
    [SerializeField] private bool _autoUpdate;
    [SerializeField] private TerrainType[] _regions;

    public bool AutoUpdate
    {
        get { return _autoUpdate; }
        private set { _autoUpdate = value; }
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(_mapWidth, _mapHeight, _seed, _noiseScale, _octaves, _persistence, _lucunarity, _offset);

        Color[] colourMap = new Color[_mapWidth * _mapHeight];
        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < _regions.Length; i++)
                {
                    if (currentHeight <= _regions[i].Height)
                    {
                        colourMap[y * _mapWidth + x] = _regions[i].Color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindFirstObjectByType<MapDisplay>();

        if (_drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (_drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, _mapWidth, _mapHeight));
        }
    }

    void OnValidate()
    {
        if (_mapWidth < 1)
        {
            _mapWidth = 1;
        }

        if (_mapHeight < 1)
        {
            _mapHeight = 1;
        }

        if (_lucunarity < 1)
        {
            _lucunarity = 1;
        }

        if (_octaves < 0)
        {
            _octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Height;
    public Color Color;
}
