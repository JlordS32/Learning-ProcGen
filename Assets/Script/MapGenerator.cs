using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    [SerializeField] private float _noiseScale;

    public void GenerateMap() {
        float [,] noiseMap = Noise.GenerateNoiseMap(_mapWidth, _mapWidth, _noiseScale);
    }
}
