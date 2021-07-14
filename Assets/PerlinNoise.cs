using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoise : MonoBehaviour
{

    public int width = 256;
    public int height = 256;

    public float scale=20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public Tilemap MountainMap;
    public Tilemap GrassMap;
    public Tilemap WaterMap;
    public Tilemap Blabla;

    public RuleTile MountainTile;
    public RuleTile GrassTile; 
    public RuleTile WaterTile;
    public RuleTile BlablaTile;


    private int[,] terrainMap;

    private float tempX;
    private float tempY;

    private void Start()
    {
        terrainMap = new int[width, height];
        GetComponent<Renderer>().material.mainTexture = GenerateTexture();
        tempX = offsetX;
        tempY = offsetY;
    
    }

    public void Update()
    {
        if (tempX != offsetX || tempY != offsetY)
        {
            tempX = offsetX;
            tempY = offsetY;
            MountainMap.ClearAllTiles();
            GrassMap.ClearAllTiles();
            WaterMap.ClearAllTiles();
            GenerateTexture();
        }

        GetComponent<Renderer>().material.mainTexture = GenerateTexture();

    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for(int y=0; y<height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;

    }


    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if (sample < 0.2)
            WaterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), WaterTile);
        else if(sample< 0.4)
            GrassMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), GrassTile);
        else if(sample< 0.8)
            Blabla.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), BlablaTile);
        return new Color(sample, sample, sample);

    }
}
