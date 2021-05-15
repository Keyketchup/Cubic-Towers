using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator singleton = null;

    public GameObject mapTile;

    public Color pathColor;
    public Color startColor;
    public Color endColor;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    public List<GameObject> mapTiles = new List<GameObject>();
    public List<GameObject> pathTiles = new List<GameObject>();

    public GameObject startTile { get; private set; }
    public GameObject endTile { get; private set; }

    private bool reachedX;
    private bool reachedY;

    private int currentIndex;
    private GameObject currentTile;
    private int nextIndex;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void moveRight()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex + 1;
        currentTile = mapTiles[nextIndex];
    }

    private void moveDown()
    {
        pathTiles.Add(currentTile);
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void generateMap()
    {

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(mapTile, this.transform);
                
                mapTiles.Add(newTile);
                newTile.layer = 8;
                newTile.tag = "Tile";

                newTile.transform.position = new Vector2(x - mapWidth / 2 + xOffset, y - mapHeight / 2 + yOffset);
            }
        }

        endTile = mapTiles[mapWidth - 2];
        startTile = mapTiles[mapHeight * mapWidth - mapWidth + 1];

        reachedX = false;
        reachedY = false;

        currentTile = startTile;
        int loopCount = 0;

        for (int i = 0; i < (mapWidth - 1) / 2; i++)
        {
            moveDown();
        }

        while (!reachedX)
        {
            loopCount++;
            if (loopCount > 1000)
            {
                Debug.Log("Loop went too long");
                break;
            }
            if (currentTile.transform.position.x != endTile.transform.position.x)
            {
                moveRight();
            }
            else
            {
                reachedX = true;
            }

        }

        while (!reachedY)
        {
            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }
            else
            {
                reachedY = true;
            }

        }

        pathTiles.Add(endTile);

        foreach (GameObject obj in pathTiles)
        {
            obj.GetComponent<Renderer>().material.color = pathColor;
            obj.tag = "Path";
            obj.layer = 9;
        }

        startTile.GetComponent<Renderer>().material.color = startColor;
        endTile.GetComponent<Renderer>().material.color = endColor;

    }

    private void Start()
    {
        Renderer objRenderer = GetComponent<Renderer>();
        generateMap();
    }

}