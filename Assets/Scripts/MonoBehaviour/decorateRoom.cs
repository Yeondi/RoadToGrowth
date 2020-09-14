using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class decorateRoom : MonoBehaviour
{
    public Tile openDoor;
    public Tile closedDoor;
    public GameObject lantern;
    public SpawnPoint spawnPoint;
    private Tilemap decoration;

    public GameObject shade;
    public GameObject bandit;

    PDG pdg;
    Player player;

    public void init()
    {
        pdg = GameObject.Find("PDG").GetComponent<PDG>();
        player = GameObject.Find("Player").GetComponent<Player>();
        decoration = pdg.CreateTilemap("decorationMap", true);
        decoration.gameObject.AddComponent<interaction>();
        decoration.tag = "interaction";
        decoration.gameObject.layer = 23;
        decoration.tileAnchor = new Vector3(0.5f, 0.11f, 0f);
        decoration.GetComponent<TilemapRenderer>().sortingOrder = -10;

        setDecoration();
        spawnEnemy();
    }

    private void setDecoration()
    {
        int SpawnPointRoomIndex = Random.Range(0, pdg.mainRooms.Count);
        int nIndex = 0;

        //Entry
        decoration.SetTile(new Vector3Int((int)player.gameObject.transform.position.x, (int)player.gameObject.transform.position.y - 1, (int)player.gameObject.transform.position.z), closedDoor);
        Instantiate(lantern, new Vector3((int)player.gameObject.transform.position.x - 1, (int)player.gameObject.transform.position.y), Quaternion.identity);
        Instantiate(lantern, new Vector3((int)player.gameObject.transform.position.x + 2, (int)player.gameObject.transform.position.y), Quaternion.identity);

        //Exit
        foreach (Room exit in pdg.mainRooms)
        {
            if (SpawnPointRoomIndex == nIndex)
            {
                if (exit.Shop == true)
                {
                    SpawnPointRoomIndex = Random.Range(0, pdg.mainRooms.Count);
                    continue;
                }
                decoration.SetTile(new Vector3Int((int)exit.pos.x + (int)exit.pos.width - 4, (int)exit.pos.y + 3, 0), openDoor);
                Instantiate(bandit, new Vector3(exit.pos.x + exit.pos.width - (exit.pos.width / 2) + 3, exit.pos.y + 3), Quaternion.identity);
                Instantiate(lantern, new Vector3((int)exit.pos.x + (int)exit.pos.width - 5, (int)exit.pos.y + 4), Quaternion.identity);
                Instantiate(lantern, new Vector3((int)exit.pos.x + (int)exit.pos.width - 2, (int)exit.pos.y + 4), Quaternion.identity);
            }
            nIndex++;
        }
    }

    private void spawnEnemy()
    {
        int nForMainRooms = Random.Range(1, 2);
        foreach (Room mainRoom in pdg.mainRooms)
        {
            for (int i = 0; i < nForMainRooms; i++)
            {
                float defaultPosX = mainRoom.pos.x + mainRoom.pos.width - 2 - i;
                SpawnPoint spawn = Instantiate(spawnPoint, new Vector3(defaultPosX, mainRoom.pos.y + 3), Quaternion.identity);
                spawnPoint.prefabToSpawn = shade;
                spawnPoint.repeatInterval = 90000f;
            }
        }
    }
}
