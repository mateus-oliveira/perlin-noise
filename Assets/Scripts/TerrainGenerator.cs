using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int depth = 20;
    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float offsetX = 100f;
    [SerializeField] private float offsetY = 100f;
    [SerializeField] private float scale = 20f;

    void Start(){
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    void Update() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = this.GenerateTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * 5f;
    }

    private TerrainData GenerateTerrain (TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, this.GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights() {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = this.CalculateHeight(x, y);
            }
        }
        return heights;
    }

    private float CalculateHeight (int x, int y) {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
