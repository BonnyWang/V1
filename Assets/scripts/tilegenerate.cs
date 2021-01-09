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
    public int[,] visionMap;

    public Vector3Int tmpSize;
    public Tilemap tMap;
    public Tilemap eMap;
    public Tilemap vMap;
    public Tilemap sMap;
    public Tile normalTile;
    public Tile mudTile;
    public Tile viewedTile;
    public Tile fogTile;
    public Tile skillrangeTile;
    public Tile skilltargetTile;
    [SerializeField] Button restartButton;
    public List<GameObject> players;
    public generatePlayer GP;
    int width;
    int height;

    public void Start()
    {
        //restartButton.onClick.AddListener(again);
        GP = GameObject.Find("generatePlayer").GetComponent<generatePlayer>();
        players = GP.players;
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

                vMap.SetTile(new Vector3Int(y, x, 0),fogTile);

            }
        }
    }

    public void generateMap()
    {
        terrainMap = new int[width, height];
        visionMap = new int[width, height];
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

                //vision map
                visionMap[x, y] = 0;
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

    public  void updateFog()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (visionMap[x,y] == 1)
                {
                    //viewed
                    visionMap[x,y] = 2;
                }
            }
        }

        //check player
        for(int i = 0; i < players.Count; i++)
        {
            Vector3Int v = players[i].GetComponent<characterControl>().cellPosition;
            int vp = Mathf.RoundToInt(players[i].GetComponent<CharacterStats>().view);
            clearFog(v.y, v.x, vp);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (visionMap[x, y] == 1)
                {
                    //view
                    vMap.SetTile(new Vector3Int(y, x, 0), null);
                }
                else if (visionMap[x, y] == 2)
                {
                    //viewed
                    vMap.SetTile(new Vector3Int(y, x, 0), viewedTile);
                }
                else if (visionMap[x, y] == 0)
                {
                    //unviewed
                    vMap.SetTile(new Vector3Int(y, x, 0), fogTile);
                }
            }
        }
    }

    void clearFog(int x, int y, int vp)
    {
        //Debug.Log(x);
        //Debug.Log(y);
        //Debug.Log(vp);
        int k = 0;
        for (int j = 0; j < vp; j++)
        {
            if (((x + j - 1) % 2) == 1 && j>0)
            {
                k++;
            }
            for (int i = 0; i < vp * 2 - 1; i++)
            {
               
                if (i>vp*2-2-j)
                {
                    //do nothing
                }
                else if(y - vp + k + i + 1 < height && y - vp + k + i + 1 > -1)
                {
                    //Debug.Log(string.Format("({0},{1})  ready", x + j, y - vp + k + i - j));
                    if (x + j < height)
                    {
                        visionMap[x + j, y - vp + k + i+1] = 1;
                       // Debug.Log(string.Format("({0},{1})  cleared", x + j, y - vp + k + i + 1));
                    }
                    if (x - j > -1)
                    {
                        visionMap[x - j, y - vp + k + i+1] = 1;
                        //Debug.Log(string.Format("({0},{1})  cleared", x - j, y - vp + k + i + 1));
                    }
                }
            }         
        }
    }

    
    
    public void updateSkill(int[,] skillMap)
    {
        Debug.Log("update skill");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (skillMap[x, y] == 1)
                {
                    //view
                    sMap.SetTile(new Vector3Int(y, x, 0), skillrangeTile);
                }

                else if (skillMap[x, y] == 0)
                {
                    //unviewed
                    sMap.SetTile(new Vector3Int(y, x, 0), null);
                }
                else 
                {
                    //viewed
                    sMap.SetTile(new Vector3Int(y, x, 0), skilltargetTile);
                }
            }
        }
    }

    public void resetSMap()
    {
        Debug.Log("smap reset");
        sMap.ClearAllTiles();

    }
}
