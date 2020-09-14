using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PDG : MonoBehaviour
{

    public float RoomRandomCircleRadius = 200.0f;
    public float ellipse_width = 400;
    public float ellipse_height = 20;
    public int RoomCount = 100;
    public int RoomMaxWidth = 30;
    public int RoomMinWidth = 3;
    public int RoomMaxHeight = 30;
    public int RoomMinHeight = 3;
    public int HallWidth = 3;
    public float MainRoomMulValue = 0.7f;
    public int HallOffsetOfEdge = 1;
    public Room roomObj;
    public CameraMove cameramove;
    public setBackGround setBackGround;
    public decorateRoom decorate;


    private List<Room> rooms = new List<Room>();
    public List<Room> mainRooms = new List<Room>();
    public List<Room> HallRooms = new List<Room>();

    public bool OnTriggerSignal;

    private class LineSegment
    {
        public Vector2 start;
        public Vector2 stop;

        public LineSegment(Vector2 start, Vector2 stop)
        {
            this.start = start;
            this.stop = stop;
        }
    }
    private Dictionary<Room, List<LineSegment>> neighborGraph = new Dictionary<Room, List<LineSegment>>();
    private Dictionary<Room, List<Room>> neighborRoomCollection = new Dictionary<Room, List<Room>>();
    private List<Room> halls = new List<Room>();
    int nHallNumbering;

    bool isOverlapped;

    [SerializeField]
    private GameObject player;

    Vector2 backupVectorValue;

    bool outLineDetected;

    //tile
    private GameObject _ladderGrid;
    Tilemap tmLadder;
    Tilemap tmMap;
    Tilemap tmHall;
    public Tile TileForDebug;
    public Tile TileForDebug2;
    public Tile TileLadder;
    public Tile[] floorTiles;
    public Tile[] cellingTiles;
    public Tile[] sideWallTiles;
    public Tile[] hallTiles;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Procedural Dungeon Generator
    public Vector2 GetRandomPointInCircle(float radius)
    {
        float t = 2 * Mathf.PI * Random.Range(0.0f, 1.0f);
        float u = Random.Range(0.0f, 1.0f) + Random.Range(0.0f, 1.0f);
        float r;
        if (u > 1)
            r = 2 - u;
        else
            r = u;

        return new Vector2(radius * r * Mathf.Cos(t), radius * r * Mathf.Sin(t));
    }

    public Vector2 GetRandomPointInEllipse(float ellipse_width, float ellipse_height)
    {
        float t = 2 * Mathf.PI * Random.Range(0.0f, 1.0f);
        float u = Random.Range(0.0f, 1.0f) + Random.Range(0.0f, 1.0f);
        float r;
        if (u > 1)
            r = 2 - u;
        else
            r = u;
        backupVectorValue = new Vector2(ellipse_width * r * Mathf.Cos(t) / 2, ellipse_height * r * Mathf.Sin(t));
        return backupVectorValue;
    }

    IEnumerator GenStep1()
    {
        rooms.Clear();
        for (int i = 0; i < RoomCount; i++)
        {
            //Vector2 r = GetRandomPointInCircle(RoomRandomCircleRadius);
            Vector2 r = GetRandomPointInEllipse(ellipse_width, ellipse_height);
            Room room = Instantiate(roomObj) as Room;
            float fWidthSize = Random.Range(RoomMinWidth, RoomMaxWidth);
            float fHeightSize = Random.Range(RoomMinHeight, RoomMaxHeight);
            room.pos = new Rect(Mathf.Round(r.x + RoomRandomCircleRadius),
                                Mathf.Round(r.y + RoomRandomCircleRadius),
                                Mathf.Round(fWidthSize),
                                Mathf.Round(fHeightSize)
                                );
            room.name = "room" + i;
            rooms.Add(room);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(serepateRooms());
    }

    private void Start()
    {
        isOverlapped = true;
        nHallNumbering = 0;
        StartCoroutine(GenStep1());
        _ladderGrid = new GameObject("Grid");
        _ladderGrid.AddComponent<Grid>();
        tmLadder = CreateTilemap("ladderTileMap", true);
        tmMap = CreateTilemap("_TileMap", false);
        tmHall = CreateTilemap("hallTileMap", false);
        GenStep1();
    }

    IEnumerator serepateRooms()
    {
        int nCount = rooms.Count;

        while (isOverlapped == true)
        {
            isOverlapped = false;
            for (int i = 0; i < nCount; i++)
            {
                Room roomA = rooms[i];
                for (int j = i + 1; j < nCount; j++)
                {
                    Room roomB = rooms[j];
                    roomA.pos.x = Mathf.Floor(roomA.pos.x);
                    roomA.pos.y = Mathf.Floor(roomA.pos.y);
                    roomB.pos.y = Mathf.Floor(roomB.pos.y);
                    roomB.pos.x = Mathf.Floor(roomB.pos.x);
                    if (overlapCheck(roomA, roomB))
                    {
                        isOverlapped = true;
                        float dx = Mathf.Min(roomA.GetRight() - roomB.GetLeft(), roomA.GetLeft() - roomB.GetRight());
                        float dy = Mathf.Min(roomA.GetBottom() - roomB.GetTop(), roomA.GetTop() - roomB.GetBottom());

                        if (Mathf.Abs(dx) < Mathf.Abs(dy))
                        {
                            float dxa = -dx / 2;
                            float dxb = dx + dxa;
                            roomA.pos.x += dxa;
                            roomB.pos.x += dxb;
                        }
                        else
                        {
                            float dya = -dy / 2;
                            float dyb = dy + dya;
                            roomB.pos.y += dyb;
                            roomA.pos.y += dya;
                        }
                    }
                }
                yield return null;
            }
        }
        selectMainRoom();
    }

    public void selectMainRoom()
    {
        Room room;
        for (int i = 0; i < rooms.Count; i++)
        {
            room = rooms[i];
            if (room.pos.width >= 15.0f && room.pos.height >= 10)
            {
                room.mainRoom = true;
                room.gameObject.tag = "mainRoom";
                mainRooms.Add(room);
            }
        }
        StartCoroutine(FindNeighbors());
    }

    bool overlapCheck(Room a, Room b)
    {
        if (a.pos.x < b.pos.x + b.pos.width
            && a.pos.y < b.pos.y + b.pos.height
            && b.pos.x < a.pos.x + a.pos.width
            && b.pos.y < a.pos.y + a.pos.height)
        {
            return true;
        }
        return false;
        //return a.pos.Overlaps(b.pos);
    }

    void Update()
    {
        foreach (Room r in neighborGraph.Keys)
        {
            foreach (LineSegment l in neighborGraph[r])
            {
                Debug.DrawLine(l.start, l.stop, new Color(1f, 1f, 0f, 0.5f));
            }
        }
    }

    private float distance(Room a, Room b)
    {
        return Mathf.Pow(a.pos.center.x - b.pos.center.x, 2) + Mathf.Pow(a.pos.center.y - b.pos.center.y, 2);
    }

    IEnumerator FindNeighbors()
    {
        Room RoomA, RoomB, RoomC;
        float Dist1, Dist2, Dist3;
        bool goNext;
        for (int i = 0; i < mainRooms.Count; i++)
        {
            RoomA = mainRooms[i];
            for (int j = i + 1; j < mainRooms.Count; j++)
            {
                goNext = false;
                RoomB = mainRooms[j];
                Dist1 = distance(RoomA, RoomB);
                for (int k = 0; k < mainRooms.Count; k++)
                {
                    if (k == i || k == j) continue;
                    RoomC = mainRooms[k];
                    Dist2 = distance(RoomA, RoomC);
                    Dist3 = distance(RoomB, RoomC);
                    if (Dist2 < Dist1 && Dist3 < Dist1)
                        goNext = true;
                    if (goNext)
                        break;
                }
                if (!goNext)
                {
                    if (!neighborGraph.ContainsKey(RoomA))
                    {
                        neighborGraph.Add(RoomA, new List<LineSegment>());
                        neighborRoomCollection.Add(RoomA, new List<Room>());
                    }
                    //neighborGraph[RoomA].Add(new LineSegment(RoomA.pos.center, RoomB.pos.center));
                    neighborGraph[RoomA].Add(new LineSegment(RoomA.GetBottomCenter(), RoomB.GetBottomCenter()));
                    neighborRoomCollection[RoomA].Add(RoomB);
                    yield return new WaitForSeconds(0.001f);
                }
            }
        }
        makeBridge();
    }

    private void makeBridge()
    {
        foreach (Room a in neighborRoomCollection.Keys)
        {
            foreach (Room b in neighborRoomCollection[a])
            {
                Room roomA, roomB;
                if (a.pos.center.x < b.pos.center.x)
                {
                    roomA = a;
                    roomB = b;
                }
                else
                {
                    roomA = b;
                    roomB = a;
                }

                float x = roomA.GetBottomCenter().x;
                float y = roomA.GetBottomCenter().y;
                float dx = roomB.GetBottomCenter().x - x;
                float dy = roomB.GetBottomCenter().y - y;
                BoxCollider2D box;
                if (Random.value > 0.5f)
                {
                    Rect Hall = new Rect(x, y, dx + 1, 1);
                    Room r = Instantiate(roomObj) as Room;
                    r.pos = Hall;
                    r.isHall = true;
                    r.name = "Hall" + nHallNumbering;
                    r.tag = "Hall";
                    box = r.gameObject.AddComponent<BoxCollider2D>();
                    box.size = new Vector2(r.pos.width, r.pos.height);
                    box.offset = new Vector2(box.size.x / 2, box.size.y / 2);
                    box.isTrigger = true;
                    nHallNumbering++;
                    decisionHorizontalOrVertical(r);
                    halls.Add(r);

                    Hall = new Rect(x + dx, y + (dy > 0 ? 0 : dy), 1, (dy > 0 ? dy : -dy));
                    r = Instantiate(roomObj) as Room;
                    r.pos = Hall;
                    r.isHall = true;
                    r.name = "Hall" + nHallNumbering;
                    r.tag = "Hall";
                    box = r.gameObject.AddComponent<BoxCollider2D>();
                    box.size = new Vector2(r.pos.width, r.pos.height);
                    box.offset = new Vector2(box.size.x / 2, box.size.y / 2);
                    box.isTrigger = true;
                    nHallNumbering++;
                    decisionHorizontalOrVertical(r);
                    halls.Add(r);
                }
                else
                {
                    Rect Hall = new Rect(x, y + dy, dx + 1, 1);
                    Room r = Instantiate(roomObj) as Room;
                    r.pos = Hall;
                    r.isHall = true;
                    r.name = "Hall" + nHallNumbering;
                    r.tag = "Hall";
                    box = r.gameObject.AddComponent<BoxCollider2D>();
                    box.size = new Vector2(r.pos.width, r.pos.height);
                    box.offset = new Vector2(box.size.x / 2, box.size.y / 2);
                    box.isTrigger = true;
                    nHallNumbering++;
                    decisionHorizontalOrVertical(r);
                    halls.Add(r);

                    Hall = new Rect(x, y + (dy > 0 ? 0 : dy), 1, (dy > 0 ? dy : -dy));
                    r = Instantiate(roomObj) as Room;
                    r.pos = Hall;
                    r.isHall = true;
                    r.name = "Hall" + nHallNumbering;
                    r.tag = "Hall";
                    box = r.gameObject.AddComponent<BoxCollider2D>();
                    box.size = new Vector2(r.pos.width, r.pos.height);
                    box.offset = new Vector2(box.size.x / 2, box.size.y / 2);
                    box.isTrigger = true;

                    nHallNumbering++;
                    decisionHorizontalOrVertical(r);
                    halls.Add(r);
                }

            }
        }
        shop();
        changeHall();
    }

    private void shop()
    {
        int nRandom = Random.Range(0, mainRooms.Count);
        mainRooms[nRandom].tag = "Shop";
        mainRooms[nRandom].mainRoom = false;
        mainRooms[nRandom].Shop = true;

        ShopManager.sharedInstance.room = mainRooms[nRandom];
        ShopManager.sharedInstance.isShopOpen = true;

    }

    private void changeHall()
    {
        foreach (Room rooms in rooms)
        {
            if (rooms.mainRoom == true)
                continue;
            foreach (Room halls in halls)
            {
                if (halls.pos.width < 6 || halls.pos.height < 6)
                    continue;
                if (overlapCheck(rooms, halls))
                {
                    rooms.tag = "HallRoom";
                    rooms.isHall = true;
                    HallRooms.Add(rooms);
                }
            }
            if (!rooms.isHall && !rooms.mainRoom && !rooms.Shop)
                rooms.disable = true;
        }
        setTilesOnMainRooms();
        setTilesOnHallRooms();
        setLadderTilesOnHalls();
        setTilesOnHorizontalHalls();
        mapCreationComplete();
    }

    private void decisionHorizontalOrVertical(Room r)
    {
        //홀이 세로홀인지 가로홀인지 판단하는 함수 decision whether it is Horizontal or Vertical?
        float width = r.pos.width;
        float height = r.pos.height;
        if (width > height)
            r.isHorizontal = true;
        else if (height > width)
            r.isVertical = true;
    }

    private void setTilesOnMainRooms()
    {
        foreach (Room main in mainRooms)
        {
            float xSize = ((main.pos.x + main.pos.width) - main.pos.x);
            float ySize = ((main.pos.y + main.pos.height) - main.pos.y);
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Tile inputValue = floorTiles[Random.Range(0, floorTiles.Length)];
                    outLineDetected = false;
                    if (i == 0 || i == main.pos.width - 1 || j == 0 || j == main.pos.height - 1)
                    {
                        inputValue = sideWallTiles[Random.Range(0, sideWallTiles.Length)];
                        outLineDetected = true;
                    }
                    else if (j != 1 && !outLineDetected)
                        continue;

                    tmMap.SetTile(new Vector3Int((int)main.pos.x + i, (int)main.pos.y + j, 0), inputValue);


                    tmMap.tag = "sideWalls";
                }
            }
        }
    }

    private void setTilesOnHallRooms()
    {
        foreach (Room hallRooms in HallRooms)
        {
            float xSize = ((hallRooms.pos.x + hallRooms.pos.width) - hallRooms.pos.x);
            float ySize = ((hallRooms.pos.y + hallRooms.pos.height) - hallRooms.pos.y);
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Tile InputValue = floorTiles[Random.Range(0, floorTiles.Length)];
                    outLineDetected = false;
                    if (i == 0 || i == hallRooms.pos.width - 1 || j == 0 || j == hallRooms.pos.height - 1)
                    {
                        InputValue = sideWallTiles[Random.Range(0, sideWallTiles.Length)];
                        outLineDetected = true;
                    }
                    else if (j != 1 && !outLineDetected)
                        continue;

                    tmMap.SetTile(new Vector3Int((int)hallRooms.pos.x + i, (int)hallRooms.pos.y + j, 0), InputValue);

                    tmMap.tag = "walls";
                }
            }
        }
    }

    private void setLadderTilesOnHalls()
    {
        foreach (Room hall in halls)
        {
            float xSize = ((hall.pos.x + hall.pos.width) - hall.pos.x);
            float ySize = ((hall.pos.y + hall.pos.height) - hall.pos.y);
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Tile inputValue = TileLadder;

                    if (hall.isHorizontal)
                        continue;

                    if (i == hall.pos.width || j == hall.pos.height)
                    {
                        inputValue = null;
                    }
                    tmLadder.SetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y + j, 0), inputValue);

                    if (tmMap.GetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y + j, 0)))
                    {
                        tmMap.SetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y + j, 0), TileForDebug2);
                        continue;
                    }
                }
            }
        }
    }

    private void setTilesOnHorizontalHalls()
    {
        foreach (Room hall in halls)
        {
            float xSize = ((hall.pos.x + hall.pos.width) - hall.pos.x) + 1;
            float ySize = ((hall.pos.y + hall.pos.height) - hall.pos.y) + 1;
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Tile inputValue = hallTiles[Random.Range(0, hallTiles.Length)];

                    if (tmMap.GetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y, 0)))
                    {
                        tmMap.SetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y, 0), TileForDebug2);
                        continue;
                    }

                    if (tmMap.GetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y - 1, 0)) != null ||
                        tmMap.GetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y - 2, 0)) != null ||
                        tmLadder.GetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y - 1, 0)) != null)
                        continue;


                    tmHall.SetTile(new Vector3Int((int)hall.pos.x + i, (int)hall.pos.y - 1, 0), inputValue);
                }
            }
        }

    }

    private void mapCreationComplete()
    {

        //GameObject.Find("ladderTileMap").AddComponent<Ladder>();

        GameObject.Find("Sub Camera").GetComponent<Camera>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
        GameObject.Find("Joystick canvas").GetComponent<Canvas>().enabled = true;

        createSpawnPoint();
        setBackGround.setMainRoomsInfo(mainRooms);
    }

    private void createSpawnPoint()
    {
        int SpawnPointRoomIndex = Random.Range(0, mainRooms.Count);
        int nIndex = 0;
        foreach (Room main in mainRooms)
        {
            if (SpawnPointRoomIndex == nIndex)
            {
                GameObject playerObject = Instantiate(player, new Vector3(main.pos.x + 4.0f, main.pos.y + 4.0f), Quaternion.identity) as GameObject;
                playerObject.name = "Player";
                playerObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
                Debug.Log("player spawned at " + playerObject.transform.position + "   " + main.name);

                Camera.main.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -10);
                Camera.main.orthographicSize = 5;

                if (cameramove != null)
                    cameramove.initScript();

                decorate.init();
                break;
            }
            nIndex++;
        }
    }

    public Tilemap CreateTilemap(string tilemapName, bool setTrigger)
    {
        var go = new GameObject(tilemapName);
        var tm = go.AddComponent<Tilemap>();
        var tr = go.AddComponent<TilemapRenderer>();
        TilemapCollider2D tc = go.AddComponent<TilemapCollider2D>();
        CompositeCollider2D cc = go.AddComponent<CompositeCollider2D>();
        Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
        tm.tag = "mainRoom";
        if (setTrigger)
        {
            //tc.isTrigger = true;
            tc.usedByComposite = true;
            cc.isTrigger = true;
            go.layer = 22;
            if (tilemapName == "ladderTileMap")
            {
                go.AddComponent<ladder>();
                tm.tag = "ladder";
                Destroy(cc);
                tc.usedByComposite = false;
                tc.isTrigger = true;
            }
        }

        tm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        go.transform.SetParent(_ladderGrid.transform);
        tr.sortingLayerName = "Main";

        return tm;
    }


}
