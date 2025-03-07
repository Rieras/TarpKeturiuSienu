using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;

    int roomWidth = 20;
    int roomHeight = 12;

    int gridSizeX = 10;
    int gridSizeY = 10;

    private List<GameObject> roomObjects = new List<GameObject>();

    private int[,] roomGrid;

    private int roomCount;
}
