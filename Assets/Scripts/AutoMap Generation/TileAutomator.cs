using UnityEngine;
using UnityEngine.Tilemaps;


public class TileAutomator : MonoBehaviour {


    [Header("Groups")]
    public GameObject MonsterGroup;
    public GameObject ItemGroup;

    [Header("Automation Options")]
    [Range(0, 100)]
    public int iniChance;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;
    [Range(1, 10)]
    public int numR;

    private int[,] terrainMap;
    public Vector3Int tmpSize;

    [Header("Tilemap Options")]
    public Tilemap topMap;
    public Tilemap botMap;
    public RuleTile[] topTile;
    public RuleTile[] botTile;
    public Tile spawnTile;

    [Header("Spawner Options")]
    public Transform playerPos;
    
    [Range(1, 10)]
    public int numOfSpawn;
    private int spawnCounter;
    public GameObject[] MonstersToSpawn;
    public GameObject[] MonsterSpawned;

    
    [Range(1, 15)]
    public int numOfItems;
    private int itemCounter;
    public GameObject[] ItemsToSpawn;
    public GameObject[] ItemsSpawned;

    private int width;
    private int height;
    private int xRand, yRand;
    
 

    public void Start()
    {
        MonsterSpawned = new GameObject[numOfSpawn];
        ItemsSpawned = new GameObject[numOfItems];
        doSim(numR);
    }

    public void doSim(int num)
    {
        clearMap(false);
        Despawner();

        spawnCounter = numOfSpawn;

        width = tmpSize.x;
        height = tmpSize.y;


        if (terrainMap == null){        
            terrainMap = new int[width, height];
            initPos();
        }

        for (int i = 0; i < num; i++){
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                if (terrainMap[x, y] == 1)
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile[0]);
                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile[0]);
            }
        }

        MonsterSpawner();
        PlayerSpawner();
        ItemSpawner();
        
    }

    private void PlayerSpawner()
    {
        do
        {
            xRand = Random.Range(0, width);
            yRand = Random.Range(0, height);
        } while (terrainMap[xRand, yRand] != 0);
        playerPos.position = botMap.GetCellCenterWorld(new Vector3Int(-xRand + width / 2, -yRand + height / 2, 0));
    }

    public void MonsterSpawner()
    {
        do//Random Spot on the ground Tilemap for enemy Spawning
        {
            xRand = Random.Range(0, width);
            yRand = Random.Range(0, height);
            if (terrainMap[xRand, yRand] == 0)
            {
                //botMap.SetTile(new Vector3Int(-xRand + width / 2, -yRand + height / 2, 0), spawnTile); //GIA NA CHEKCARO TA SPAWN POINTS OTAN XREIAZETAI
                GameObject Monster = Instantiate(MonstersToSpawn[Random.Range(0, MonstersToSpawn.Length)], botMap.GetCellCenterWorld(new Vector3Int(-xRand + width / 2, -yRand + height / 2, 0)), Quaternion.identity,MonsterGroup.transform) as GameObject;
                MonsterSpawned[numOfSpawn - spawnCounter] = Monster;
                spawnCounter--;
            }
        } while (spawnCounter != 0);
    }

    public void ItemSpawner()
    {
        int num = numOfItems;
        do
        {
            xRand = Random.Range(0, width);
            yRand = Random.Range(0, height);
            if (terrainMap[xRand, yRand] == 0 && terrainMap[xRand, yRand + 1] == 1)
            {
                GameObject Light = Instantiate(ItemsToSpawn[Random.Range(0, ItemsToSpawn.Length)], botMap.GetCellCenterWorld(new Vector3Int(-xRand + width / 2, -yRand + height / 2, 0)), Quaternion.identity,ItemGroup.transform) as GameObject;
                ItemsSpawned[numOfItems - num] = Light;
                num--;
            }
        } while (num > 0);
    }

    public void Despawner()
    {
        foreach (GameObject i in ItemsSpawned){Destroy(i);}
        foreach (GameObject i in MonsterSpawned){Destroy(i);}
    }

    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }
    }

    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                        neighb += oldMap[x + b.x, y + b.y];
                    else
                        neighb++;
                }
                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit) 
                        newMap[x, y] = 0;
                    else                 
                        newMap[x, y] = 1;
                }
                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) 
                        newMap[x, y] = 1;
                    else
                        newMap[x, y] = 0;
                }
            }
        }
        return newMap;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            doSim(numR);
        }


        if (Input.GetMouseButtonDown(1))
        {
            clearMap(true);
            Despawner();
        }
        if (Input.GetMouseButton(2))
        {
            //SaveAssetMap();
            //count++;
        }
    }

    /*public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.SaveAsPrefabAsset())
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
            }


        }


    }*/

    public void clearMap(bool complete)
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }
    }
}
