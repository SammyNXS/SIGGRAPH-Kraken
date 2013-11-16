using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public float playerSpeed = 10f;
	public Vector3 vec;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") < 0)
		{
			Debug.Log("Left");
			vec = new Vector3(-20f, 0, 0);
			rigidbody.AddForce(vec);
		}
		if(Input.GetAxis("Horizontal") > 0)
		{
			Debug.Log("Right");
			vec = new Vector3(20f, 0, 0);
			rigidbody.AddForce(vec);
		}
		if(Input.GetButtonDown ("Jump"))
		{
			Debug.Log ("Space");
			vec = new Vector3(0, 200f, 0);
			rigidbody.AddForce(vec);
		}
	}
}