using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class setBackGround : MonoBehaviour
{
    private List<Room> mainCopy = new List<Room>();
    public Tile[] backGroundTiles;
    private bool outLineDetected;

    private Tilemap backgroundMap;

    public void setMainRoomsInfo(List<Room> mainRooms)
    {
        mainCopy = mainRooms;
        Debug.Log("Copy Complete. Total Length : " + mainCopy.Count);

        backgroundMap =  GameObject.Find("PDG").GetComponent<PDG>().CreateTilemap("backgroundMap", true);
        backgroundMap.GetComponent<TilemapRenderer>().sortingOrder = -15;
        backgroundMap.gameObject.layer = 0;
        setBackGroundTiles();
    }


    private void setBackGroundTiles()
    {
        foreach (Room main in mainCopy)
        { 
            float xSize = ((main.pos.x + main.pos.width) - main.pos.x) + 1;
            float ySize = ((main.pos.y + main.pos.height) - main.pos.y) + 1;
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    Tile inputValue = backGroundTiles[Random.Range(0, backGroundTiles.Length)];
                    outLineDetected = false;
                    if (i == 0 || i == main.pos.width || j == 0 || j == main.pos.height)
                    {
                        inputValue = null;
                        outLineDetected = true;
                    }

                    backgroundMap.SetTile(new Vector3Int((int)main.pos.x + i, (int)main.pos.y + j, 0), inputValue);


                    backgroundMap.tag = "background";
                }
            }
        }
    }
}
