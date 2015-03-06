using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{
	private float hRotation, vRotation;
	private RaycastHit hit;
	private GameObject buildObject;
    public Transform tPrefab, turretReal;
    public bool placingStage;
    private Vector3 gridPos;
    public GameObject grid;
    public GameObject wallPoint;
    private int currentBuildObj;
    private Placement place;
    
	void Start () 
	{
		hRotation = 0f;
		vRotation = 0f;
        placingStage = false;
        grid = GameObject.FindGameObjectWithTag("Ground");
        currentBuildObj = 0;
        place = new Placement(grid);
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

        if (Input.GetKeyDown(KeyCode.P) && !placingStage)
        {
            placingStage = true;
            grid.GetComponent<Grid>().ActivateGrid();
        }

        if(placingStage)
            placing();


    }

    void placing()
    {
        if (place.Update(transform) == 1)
            placingStage = false;
    }

    public void RecieveGridPos(Vector2 pos)
    {
        gridPos.x = pos.x;
        gridPos.z = pos.y;
    }

}
