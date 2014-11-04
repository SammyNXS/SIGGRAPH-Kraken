using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{
	private float hRotation, vRotation;
	private RaycastHit hit;
	public Transform turret;
	
	void Start () 
	{
		hRotation = 0f;
		vRotation = 0f;
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
		transform.Translate(new Vector3(xAxisValue * 0.1f, yAxisValue * 0.1f, zAxisValue * 0.1f));
    }

    void FixedUpdate()
    {
        //press p to put down turret
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P WAS PRESSED");
            //if raycast hits something gets information and puts in hit, hit carries the position of the raycast collision
            if (Physics.Raycast(transform.FindChild("placementPointer").transform.position, transform.forward, out hit))
            {
                Instantiate(turret, hit.point, Quaternion.identity);
            }
        }
    }
}
