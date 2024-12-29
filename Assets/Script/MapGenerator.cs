using UnityEngine;

public class MapGenerator : MonoBehaviour
{
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

    public bool AutoUpdate
    {
        get { return _autoUpdate; }
        private set { _autoUpdate = value; }
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(_mapWidth, _mapHeight, _seed, _noiseScale, _octaves, _persistence, _lucunarity, _offset);

        MapDisplay display = FindFirstObjectByType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
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
