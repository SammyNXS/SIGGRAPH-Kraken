using UnityEngine;
using System.Collections;

public class simpleenemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(-10f*Time.deltaTime, 0, 0);
	}

	void OnCollisionEnter(Collision other)
	{
		GameObject playerObject = GameObject.Find ("Player");
		if (other.gameObject.name == "Player") 
		{
			Debug.Log (other);
			Debug.Log ("Hit!");
			Destroy (playerObject);
		}

	}
}
