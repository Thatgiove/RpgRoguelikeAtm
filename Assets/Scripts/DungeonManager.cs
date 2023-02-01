using Pathfinding;
using System.Drawing;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] int rows = 2;
    [SerializeField] int cols = 2;
    [SerializeField] float XOffset = 5f;
    [SerializeField] float YOffset = 5f;
    [SerializeField] GameObject room;
    [SerializeField] GameObject level;
    [SerializeField] GameObject enemy; //TODO togliere 
    [SerializeField] GameObject weaponTemplate;

    [SerializeField] GameObject gameArea;

    public Vector3 spawnPoint = Vector3.zero;
    public GameObject test;

    float height = 0;
    float width;

    float[] angles = { 90f, -90f, 180f, -180f };

    void Awake()
    {

    }
    void Start()
    {
        GenerateDungeon();
        GeneratePathFinding();
    }



    void GenerateDungeon()
    {

        if (!room) return;
        var cu = Vector3.zero;
        for (int i = 0; i < rows; i++)
        {

            for (int j = 0; j < cols; j++)
            {

                var _room = Instantiate(room, Vector3.zero, Quaternion.identity);

                _room.transform.RotateAround(room.transform.GetChild(0).transform.position, Vector3.forward, angles[Random.Range(0, angles.Length)]);

                _room.transform.position = new Vector2(
                          _room.transform.position.x + (i * YOffset),
                           _room.transform.position.y + (j * YOffset));
                cu += _room.transform.position;

                if (i == (rows / 2) && j == (cols / 2))
                {
                    spawnPoint = _room.transform.position;
                }

                if (level)
                {
                    _room.transform.parent = level.transform;
                }

                GenerateWeapon(_room.transform.GetChild(0).transform.position);
                //if (FlipCoinHead())
                //{
                //    if (enemy)
                //    {
                //        Instantiate(enemy, _room.transform.position, Quaternion.identity);
                //    }
                //}
            }
        }


        if (test)
        {
            test.transform.localScale = cu / (rows * cols / 2f);
            test.transform.position = spawnPoint;
        }
        
    }

    void GeneratePathFinding()
    {
        // This holds all graph data
        AstarData data = AstarPath.active.data;

        // This creates a Grid Graph
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

        gg.is2D = true;

        gg.collision.use2D = true;
        gg.collision.diameter = .8f;
        gg.collision.mask = LayerMask.GetMask("Room");

        // Setup a grid graph with some values
        int width = (int)gameArea.transform.localScale.x * 2;
        int depth = (int)gameArea.transform.localScale.y * 2;
        float nodeSize = 1f;

        gg.center = new Vector3(spawnPoint.x, spawnPoint.y, 0);

        // Updates internal size from the above values
        gg.SetDimensions(width, depth, nodeSize);

        // Scans all graphs
        AstarPath.active.Scan();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO - per questione di ordine devono essere tutte figlie di un gameObj
    void GenerateWeapon(Vector3 pos)
    {
        if (!weaponTemplate) return;

        if (FlipCoinHead())
        {
            Instantiate(weaponTemplate, pos, Quaternion.identity);
        }
    }

    bool FlipCoinHead()
    {
        if (Random.value > 0.5f)
        {
            return true;
        }
        return false;
    }

}
