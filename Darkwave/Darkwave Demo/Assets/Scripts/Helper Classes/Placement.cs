using UnityEngine;
using System.Collections;


public class Placement
{
    private GameObject m_grid;
    private int m_buildNum;//number representing what building is showing
    private GameObject m_tempBuilding;//game object of what is showing
    private GameObject m_tempWallPrefab;
    private GameObject m_WallPrefab;
    private GameObject m_tempTurretPrefab;
    private GameObject m_turretPrefab;
    private Vector3 m_pos;

    public Placement(GameObject grid)
    {
        m_grid = grid;
        m_buildNum = 1;
        m_tempTurretPrefab = (GameObject)Resources.Load("Prefabs/PlacementTurret", typeof(GameObject));
        m_turretPrefab = (GameObject)Resources.Load("Prefabs/Turret3.0", typeof(GameObject));
        m_tempWallPrefab = (GameObject)Resources.Load("Prefabs/BasicWallPlacement", typeof(GameObject));
        m_WallPrefab = (GameObject)Resources.Load("Prefabs/BasicWall", typeof(GameObject));
    }
  
	// should only be called while building
	public int Update (Transform pos) 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_buildNum = 1;
            GameObject.Destroy(m_tempBuilding);
            m_tempBuilding = null;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_buildNum = 2;
            GameObject.Destroy(m_tempBuilding);
            m_tempBuilding = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(pos.position, pos.forward, out hit))
        {
            if (m_grid.GetComponent<Grid>().canPlace(hit.point))
            {
                if (m_tempBuilding == null)
                {
                    m_pos = m_grid.GetComponent<Grid>().getVector3(hit.point);//runs function to find 
                    m_pos.y = hit.point.y + 1;
                    if(m_buildNum == 1)
                        m_tempBuilding = (GameObject)Object.Instantiate(m_tempTurretPrefab, m_pos, Quaternion.identity);
                    if(m_buildNum == 2)
                        m_tempBuilding = (GameObject)Object.Instantiate(m_tempWallPrefab, m_pos, Quaternion.identity);
                }

                else
                {
                    m_pos = m_grid.GetComponent<Grid>().getVector3(hit.point);//runs function to find 
                    m_pos.y = hit.point.y + 1;
                    m_tempBuilding.transform.position = m_pos;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (m_buildNum == 1)
                        GameObject.Instantiate(m_turretPrefab, m_pos, Quaternion.identity);
                    if (m_buildNum == 2)
                        GameObject.Instantiate(m_WallPrefab, m_pos, Quaternion.identity);
                    GameObject.Destroy(m_tempBuilding);
                    m_tempBuilding = null;
                    m_grid.GetComponent<Grid>().place(m_pos);
                    m_grid.GetComponent<Grid>().DeactivateGrid();
                    return 1;
                }
            }
            else
            {
                GameObject.Destroy(m_tempBuilding);
                m_tempBuilding = null;
            }
        }
        return 0;
	}
}
