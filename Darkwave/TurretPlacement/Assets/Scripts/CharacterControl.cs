using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{
	private float hRotation, vRotation;
	private RaycastHit hit;
	private GameObject turret;
    public Transform tPrefab, turretReal;
    public bool placingStage;
    private Vector3 gridPos;
    public GameObject grid;
	
	void Start () 
	{
		hRotation = 0f;
		vRotation = 0f;
        placingStage = false;
        grid = GameObject.FindGameObjectWithTag("Ground");
	}

	void Update () 
	{
		float horizontalSpeed = 7.0F;
		float verticalSpeed = 7.0F;

		//Rotates Player on "X" Axis Acording to Mouse Input
		hRotation = (hRotation + horizontalSpeed * Input.GetAxis("Mouse X"))%360;

		//Rotates Player on "Y" Axis Acording to Mouse Input
		vRotation = Mathf.Clamp(vRotation - verticalSpeed * Input.GetAxis("Mouse Y"), -90, 90);

		//applies rotation
		transform.localEulerAngles = new Vector3(vRotation, hRotation, 0);

		//gets WASD input
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");

		float yAxisValue = Input.GetAxis("Jump");

		//applys translations(moves character)
		transform.Translate(new Vector3(xAxisValue * 0.8f, yAxisValue * 0.8f, zAxisValue * 0.8f));

        if (Input.GetKeyDown(KeyCode.P))
        {
            placingStage = true;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                turret = Instantiate(tPrefab.gameObject, hit.point, Quaternion.identity) as GameObject;
            }
        }

        if(placingStage)
            placing();


    }

    void placing()
    {
        float yValue = 0.0f;
        grid.SendMessage("building", true);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            grid.SendMessage("RecievePosition", hit.point, SendMessageOptions.RequireReceiver);
            yValue = hit.point.y;
            yValue += 0.8f;
        }

            gridPos.y = yValue;
            turret.transform.position = gridPos;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(turretReal, gridPos, Quaternion.identity);
            Destroy(turret);
            placingStage = false;
            grid.SendMessage("building", false);
        }

    }

    public void RecieveGridPos(Vector2 pos)
    {
        gridPos.x = pos.x;
        gridPos.z = pos.y;
    }

}
