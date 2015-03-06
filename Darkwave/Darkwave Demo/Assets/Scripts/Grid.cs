using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//grid starts at the bottom left corner of model

public class Grid : MonoBehaviour 
{
    public int row, col;
    public Transform marker, player;

    private float squareArea, width, height;
    private Vector3 start, placementPos, center, size;
    private bool buildingStage;
    private List<GameObject> gridLasers;
    private Vector2 lastGridPos;
    private bool[,] canPlaceArray;

	// Use this for initialization
	void Start () 
    {
        buildingStage = false;
        size = gameObject.GetComponent<Renderer>().bounds.size;
        width = size.x / row;
        height = size.z / col;
        center = new Vector3(gameObject.transform.position.x, 1.0f, gameObject.transform.position.z);
        start = new Vector3(center.x - (size.x / 2), 1.0f, center.z - (size.z / 2));
        gridLasers = new List<GameObject>();
        canPlaceArray = new bool[row, col];

        //sets up canPlace array
        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
                canPlaceArray[i, j] = true;

        //Lines along x axis
        for (int i = 0; i <= row; i++)
        {
            Vector3 rowPos = start;
            rowPos.x += width * i;
            //creates grid line
            gridLine(rowPos, new Vector3(rowPos.x, 1.0f, center.z + size.z/2));
        }

        //Lines along z axis
        for (int j = 0; j <= col; j++)
        {
            Vector3 colPos = start;
            colPos.z += height * j;
            //creates grid line
            gridLine(colPos, new Vector3(center.x + size.x/2, 1.0f, colPos.z));
        }

        foreach (GameObject line in gridLasers)
        {
            line.SetActive(false);
        }
	}

    // Update is called once per frame
    void Update() 
    {

	}

    //creates and stores the grid lines in an list
    void gridLine(Vector3 start, Vector3 end)
    {
        LineRenderer line;
        line = marker.transform.GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        gridLasers.Add(GameObject.Instantiate(marker.gameObject, start, Quaternion.identity) as GameObject);
    }

    //returns the row and col number
    public Vector2 getGridRowCol(Vector3 position)
    {
        Vector2 gridPos = new Vector2(-1, -1);

        //gets row
        for (int i = 0; i <= row + 1; i++)
        {
            if (position.x > start.x + (width * i) && position.x <  start.x + (width * (i + 1)))
            {
                gridPos.x = i;
            }
        }

        //gets cols
        for (int j = 0; j <= col; j++)
        {
            if (position.z > start.z + (height * j) && position.z <  start.z + (height * (j + 1)))
            {
                gridPos.y = j;
            }
        }

        return gridPos;
    }

    public Vector3 getVector3(Vector3 position)
    {
        Vector3 gridPos = new Vector3(-1, 0, -1);

        //gets row
        for (int i = 0; i <= row + 1; i++)
        {
            if (position.x > start.x + (width * i) && position.x < start.x + (width * (i + 1)))
            {
                gridPos.x = i;
            }
        }

        //gets cols
        for (int j = 0; j <= col; j++)
        {
            if (position.z > start.z + (height * j) && position.z < start.z + (height * (j + 1)))
            {
                gridPos.y = j;
            }
        }

        gridPos.x = (start.x + (gridPos.x * width)) + width / 2;
        gridPos.z = (start.z + (gridPos.y * height)) + height / 2;

        return gridPos;
    }

    public void place(Vector3 position)
        //takes grid position and makes lets canPlace know that a building is in the 
    {
        Vector2 gridPos = new Vector3(-1, -1);

        //gets row
        for (int i = 0; i <= row + 1; i++)
        {
            if (position.x > start.x + (width * i) && position.x < start.x + (width * (i + 1)))
            {
                gridPos.x = i;
            }
        }

        //gets cols
        for (int j = 0; j <= col; j++)
        {
            if (position.z > start.z + (height * j) && position.z < start.z + (height * (j + 1)))
            {
                gridPos.y = j;
            }
        }
        canPlaceArray[(int)gridPos.x, (int)gridPos.y] = false;//sets canPlace to false
    }

    //activates grid lines
    public void ActivateGrid()
    {
        foreach (GameObject line in gridLasers)
        {
            line.SetActive(true);
        }
    }

    //deactivates grid lines
    public void DeactivateGrid()
    {
        foreach (GameObject line in gridLasers)
        {
            line.SetActive(false);
        }
    }

    public bool canPlace(Vector3 position)
    {
        Vector2 gridPos = new Vector2(-1, -1);

        //gets row
        for (int i = 0; i <= row + 1; i++)
        {
            if (position.x > start.x + (width * i) && position.x < start.x + (width * (i + 1)))
            {
                gridPos.x = i;
            }
        }

        //gets cols
        for (int j = 0; j <= col; j++)
        {
            if (position.z > start.z + (height * j) && position.z < start.z + (height * (j + 1)))
            {
                gridPos.y = j;
            }
        }

        if (gridPos.x == -1 || gridPos.y == -1)
            return false;
        else
            return canPlaceArray[(int)gridPos.x, (int)gridPos.y];

    }
}
