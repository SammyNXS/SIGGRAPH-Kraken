using UnityEngine;
using System.Collections;

public class MagicBallPlat : Entity {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log ("standing on ball");
			float yDown = col.contacts[0].normal.y;
			if(yDown < 0)
				col.transform.parent = transform;
		}
		else if(col.gameObject.tag == "Death")
			health = 0;
	}

	//Function resets terrainTouch and enviromental movement variables
	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			if(transform.childCount > 0)
				transform.GetChild(0).gameObject.transform.parent = null;
		}
	}
}
