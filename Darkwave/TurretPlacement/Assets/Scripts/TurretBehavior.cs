using UnityEngine;
using System.Collections;

public class TurretBehavior : MonoBehaviour 
{
	public GameObject Target;
	public Transform shot;
	private bool placed;
	private int time;
	private float turnSpeed;
	public GameObject player;
    
    // Use this for initialization
	void Start () 
	{
		Target = null;
		time = 0;
		placed = false;

		player = GameObject.Find("Player");

		//makes sure theres no rotation
		transform.rotation = Quaternion.identity;

		//makes sure that turret is on ground
		Vector3 temp = transform.position;
		temp.y = 0.4f;
		transform.position = temp;

		//changes look for placing
		gameObject.renderer.material.shader = Shader.Find("Transparent/Diffuse");
		gameObject.renderer.material.color = Color.cyan;

        //turn off so collider doesnt interfere with raycast
        gameObject.collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if turret has been placed
        if (placed)
        {
            if (Target != null)
            {
                // point at target
                transform.LookAt(Target.transform);
                time++;

                //lets time pass before another shot can be taken
                if (time > 10)
                {
                    Instantiate(shot, transform.FindChild("bulletSpawn1").transform.position, transform.rotation);
                    Instantiate(shot, transform.FindChild("bulletSpawn2").transform.position, transform.rotation);
                    time = 0;
                }
            }
        }
        else
            placement();

	}

    void placement()
    {
            RaycastHit tempPos;
            Vector3 vecTemp;

            //if raycast hits something gets information and puts in tempPos, tempPos carries the position of the raycast collision
            if (Physics.Raycast(player.transform.FindChild("placementPointer").position, player.transform.forward, out tempPos))
            {
                vecTemp = tempPos.point;
                vecTemp.y = 0.4f;
                transform.position = vecTemp;
            }

            //if left mouse
            if (Input.GetMouseButtonDown(0))
            {
                placed = true;
                gameObject.renderer.material.shader = Shader.Find("Diffuse");
                gameObject.renderer.material.color = Color.gray;
                //turn collider back on
                gameObject.collider.enabled = true;
            }
    }

	void OnTriggerStay(Collider other) 
	{
		//if previous target died while in collider get a new one if in range
		if(other.gameObject.tag == "Enemy" && Target == null)
			Target = other.gameObject;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == Target)
			Target = null;
	}
}
