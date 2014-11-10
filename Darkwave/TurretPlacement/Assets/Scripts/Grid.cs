using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//grid starts at the bottom left corner of ground model

public class Grid : MonoBehaviour 
{
    public int row, col;
    public Transform marker, player;

    private float squareArea, width, height;
    private Vector3 start, placementPos, center, size;
    private bool buildingStage;
    private List<GameObject> grid;

	// Use this for initialization
	void Start () 
    {
        buildingStage = false;
        size = gameObject.renderer.bounds.size;
        width = size.x / row;
        height = size.z / col;
        center = new Vector3(gameObject.transform.position.x, 1.0f, gameObject.transform.position.z);
        start = new Vector3(center.x - (size.x / 2), 1.0f, center.z - (size.z / 2));
        grid = new List<GameObject>();

        //markers along x axis
        for (int i = 0; i <= row; i++)
        {
            Vector3 rowPos = start;
            rowPos.x += width * i;
            //creates grid line
            gridLine(rowPos, new Vector3(rowPos.x, 1.0f, center.z + size.z/2));
        }
        //markers along z axis
        for (int j = 0; j <= col; j++)
        {
            Vector3 colPos = start;
            colPos.z += height * j;
            //creates grid line
            gridLine(colPos, new Vector3(center.x + size.x/2, 1.0f, colPos.z));
        }

        foreach (GameObject line in grid)
        {
            line.SetActive(false);
        }
	}

    // Update is called once per frame
    void Update() 
    {
        if (buildingStage)
        {
            //sends row/col postion to player
            sendPos(getRowCol(placementPos));
            //turns grid on
            foreach(GameObject line in grid)
            {
                line.SetActive(true);
            }
        }

        else
        {
            //turns grid off
            foreach (GameObject line in grid)
            {
                line.SetActive(false);
            }
        }
	}

    //creates and stores the grid lines in an list
    void gridLine(Vector3 start, Vector3 end)
    {
        LineRenderer line;
        line = marker.transform.GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        grid.Add(GameObject.Instantiate(marker.gameObject, start, Quaternion.identity) as GameObject);
    }

    //returns the row and col number
    private Vector2 getRowCol(Vector3 position)
    {
        Vector2 rowCol = new Vector2(-1, -1);

        //gets row
        for (int i = 0; i <= row + 1; i++)
        {
            if (position.x > start.x + (width * i) && position.x <  start.x + (width * (i + 1)))
            {
                rowCol.x = i;
            }
        }

        //gets cols
        for (int j = 0; j <= col; j++)
        {
            if (position.z > start.z + (height * j) && position.z <  start.z + (height * (j + 1)))
            {
                rowCol.y = j;
            }
        }

        return rowCol;
    }

    //sends position turret should be placed to player
    private void sendPos(Vector2 rowCol)
    {
        float posX, posY;

        posX = (start.x + (rowCol.x * width)) + width/2;
        posY = (start.z + (rowCol.y * height)) + height/2;
        //gets center position of square
        Vector2 pos = new Vector2(posX, posY);
        player.gameObject.SendMessage("RecieveGridPos", pos);
    }

    //gets players raycast hit position
    public void RecievePosition(Vector3 pos)
    {
        placementPos = pos;
    }

    //retrieves true from player if turret placement has started
    public void building(bool build)
    {
        buildingStage = build;
    }
}
