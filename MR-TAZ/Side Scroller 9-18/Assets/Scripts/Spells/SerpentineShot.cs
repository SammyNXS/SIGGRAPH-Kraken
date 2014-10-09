using UnityEngine;
using System.Collections;

public class SerpentineShot : Shot
{
	int rotationSpeed = 1;
	// Use this for initialization
	void Start () 
	{
		health = maxHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		touchDamage = health;
		Movement ();
		ChangeRotation();
		OffScreenBehavior();
	}

	void ChangeRotation()
	{
		if(gameObject.transform.eulerAngles.z > 30 || gameObject.transform.eulerAngles.z < -30)
			rotationSpeed *= -1;
		transform.Rotate(0,0, rotationSpeed);
	}

	//Controls the shot's behavior when it hits something
	void OnCollisionEnter(Collision col)
	{
		//Player layer object vs enemy layer object
		if((gameObject.layer == 8) && (col.gameObject.layer == 9) ||
		   (gameObject.layer == 9) && (col.gameObject.layer == 8))
		{
			gameObject.GetComponent<Entity>().health -= col.gameObject.GetComponent<Entity>().touchDamage;
			
			//If two shots hit the one with lower power will zero out it's health
			if(col.gameObject.tag == "Shot" && (gameObject.GetComponent<Entity>().health <= col.gameObject.GetComponent<Entity>().health))
				gameObject.GetComponent<Entity>().health = 0;
		}
		//If a shot hits the terrain it will zero out it's health
		else if(col.gameObject.layer == 0)
			gameObject.GetComponent<Entity>().health = 0;
	}
}
