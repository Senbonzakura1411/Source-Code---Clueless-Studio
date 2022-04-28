using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    int i = 0;
    public static Vector3[] gridCenters;
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridCenters = new Vector3[width*height + 1];
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //debugTextArray[x, y] = CreateText.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 5, Color.white, TextAnchor.MiddleCenter);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                //Debug.Log(GetCenterPosition(GetWorldPosition(x, y), new Vector3(cellSize, cellSize)));
                i++;
                gridCenters[i] = GetCenterPosition(GetWorldPosition(x, y), new Vector3(cellSize, cellSize));
            }
        }
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);


    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
        
    }

    private Vector3 GetCenterPosition(Vector3 WorldPosition, Vector3 OriginPosition)
    {
       return (WorldPosition + OriginPosition / 2);
    }

   

}