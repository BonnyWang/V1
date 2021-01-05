using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Microsoft.Win32.SafeHandles;
using System;

public class characterControl : MonoBehaviour
{
    public int x = 5;
    public int y = 10;
    public bool isMoving = false;
    Vector3 destPosition;
    Vector3 tempPosition;
    Vector3Int cellPosition;
    Vector3Int targetPosition;
    [SerializeField] float step;
    [SerializeField] int APRange;
    bool canMove =true;
    int availablePoint;
    GridLayout gridLayout;
    [SerializeField] Text apText;
    [SerializeField] Button restartButton;
    [SerializeField] tilegenerate map;
    public Tilemap rMap;
    public Tile route;
    int width;
    int height;
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private List<PathNode> path;
    public int APcost;
    // Start is called before the first frame update
    void Start()
    {   
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Debug.Log(("%f oh mama",x));
        //placing x and y to initial point in formation and check availability
        width = map.tmpSize.x;
        height = map.tmpSize.y;
        cellPosition = new Vector3Int(y,x, 0);

        //placing character
        transform.position = gridLayout.CellToWorld(cellPosition);
    }

    // Update is called once per frame
    void Update()
    {   

        if (canMove)
        {
            if (Input.GetButtonDown("Click") && isMoving==false)
            {
                destPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //print(destPosition);
                targetPosition = gridLayout.WorldToCell(destPosition);
                print(targetPosition);

                pathFinding();
                //route made
                //text
                apText.text = (availablePoint, "-", APcost).ToString();
            }k
            if(Input.GetButtonDown("Click") && isMoving == true)
            {

            }

            


            

        }
    }

    void gernerateAP()
    {
        availablePoint = UnityEngine.Random.Range(1, APRange);
        apText.text = availablePoint.ToString();
    }

    void updateAPtext()
    {
        apText.text = availablePoint.ToString();
    }

    void restartTurn()
    {
        canMove = true;
        gernerateAP();
    }

    void endTurn()
    {
        canMove = false;
        destPosition = transform.position;
    }

    void pathFinding()
    {

        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));

        Debug.Log(cellPosition.y);
        Debug.Log(cellPosition.x);
        Debug.Log(targetPosition.y);
        Debug.Log(targetPosition.x);
        path = FindPath(cellPosition.y, cellPosition.x, targetPosition.y, targetPosition.x);
        if (path != null)
        {
            for(int i=0; i < path.Count; i++)
            {
                //drawtile
                Debug.Log((path[i].x,' ', path[i].y,' ', map.terrainMap[path[i].x, path[i].y]));
                rMap.SetTile(new Vector3Int(path[i].y, path[i].x, 0), route);
            }
        }


    }


    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;

            }
        }

        PathNode startNode = grid.GetGridObject(startX, startY);
        //Debug.Log(startNode.y);
        PathNode endNode = grid.GetGridObject(endX, endY);
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();



        startNode.gCost = 0;
        startNode.hCost = distanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            if(currentNode == endNode)
            {
                APcost = currentNode.gCost;
                print(("ap",APcost));
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighNode)) continue;
                //i plus 2
                int tentativeGCost = currentNode.gCost + map.terrainMap[neighNode.x, neighNode.y] + 2 ;

                if (tentativeGCost < neighNode.gCost)
                {
                    neighNode.cameFromNode = currentNode;
                    neighNode.gCost = tentativeGCost;
                    neighNode.hCost = distanceCost(neighNode, endNode);
                    neighNode.CalculateFCost();

                    if (!openList.Contains(neighNode))
                    {
                        openList.Add(neighNode);
                    }
                }
            }

        }
        return null;

    }


    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        
        path.Reverse();
        return path;
    }

    private int distanceCost(PathNode a, PathNode b)
    {
        int dc;
        if (a.x%2 == b.x%2)
        {
            dc = Math.Abs(a.x - b.x);

            if (b.y < a.y - dc / 2)
            {
                dc = dc + a.y - dc / 2- b.y ;
            }
            if(b.y > a.y + dc / 2)
            {
                dc = dc + b.y - dc / 2 - a.y;
            }

        }
        else
        {
            dc = Math.Abs(a.x - b.x);

            if (a.x % 2 == 1)
            {
                if (b.y < a.y - (dc-1) / 2)
                {
                    dc = dc + a.y - (dc-1) / 2 - b.y;
                }
                if (b.y > a.y + (dc+1) / 2)
                {
                    dc = dc + b.y - (dc+1) / 2 - a.y;
                }
            }

            else
            {
                if (b.y < a.y - (dc + 1) / 2)
                {
                    dc = dc + a.y - (dc + 1) / 2 - b.y;
                }
                if (b.y > a.y + (dc - 1) / 2)
                {
                    dc = dc + b.y - (dc - 1) / 2 - a.y;
                }
            }
        }
        return 2*dc;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        //UP
        if (currentNode.y + 1 < grid.GetHeight())
        {
            //check avaiablility
            neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
        }
        //Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));


        if (currentNode.x - 1 >= 0)
        {
            //left up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                if (currentNode.x % 2 == 1)
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y ));
                }
            }
            //left down
            if (currentNode.y - 1 >= 0)
            {
                if (currentNode.x % 2 == 1)
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y-1));
                }
            }
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            //right up
            if (currentNode.y + 1 < grid.GetHeight())
            {
                if (currentNode.x % 2 == 1)
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
                }
            }
            //right down
            if (currentNode.y - 1 >= 0)
            {
                if (currentNode.x % 2 == 1)
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
                }
                else
                {
                    neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
                }
            }
        }
        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }
}
