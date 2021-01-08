using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;


public class tilegenerate : MonoBehaviour
{

    [Range(0, 10)]
    public int mudRatio;

    [Range(1, 10)]
    public int numR;
    private int count = 0;

    public int[,] terrainMap;
    public int[,] effectMap;
    public int[,] heightMap;

    public Vector3Int tmpSize;
    public Tilemap tMap;
    public Tilemap eMap;
    public Tile normalTile;
    public Tile mudTile;
    [SerializeField] Button restartButton;
    int width;
    int height;

    public void Start()
    {
        //restartButton.onClick.AddListener(again);
        again();
    }
    public void again()
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        generateMap();

        //check the map see if reasonable, otherwise regenerate
        //checkmap();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {   
                //yxz for turning round
                if (terrainMap[x, y] == 0)
                {
                    tMap.SetTile(new Vector3Int(y, x, 0), normalTile);
                }
                else
                {
                    tMap.SetTile(new Vector3Int(y, x, 0), mudTile);
                }

            }
        }
    }

    public void generateMap()
    {
        terrainMap = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //read big map, do something
                //terrain map, 0 is normal (2AP), 1 is mud (3AP)

                if (Random.Range(1, 11) > (10 - mudRatio - 1))
                {
                    terrainMap[x, y] = 1;
                }
                else
                {
                    terrainMap[x, y] = 0;
                }

                // effect map
                //effectMap[x, y] = 0;

                //height map
                
            }
        }
        Debug.Log("generated");

    }

public void clearMap(bool complete)
    {
        tMap.ClearAllTiles();

        if (complete)
        {
            tMap = null;
        }
    }

}
