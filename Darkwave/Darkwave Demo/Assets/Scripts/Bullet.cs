using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	float timeLimit, time;

	// Use this for initialization
	void Start () 
	{
		//limits life of bullet
		timeLimit = 50;
		time = 0;
		//adds forward momentum
		GetComponent<Rigidbody>().AddForce(transform.forward * 500);
	}
	
	// Update is called once per frame
	void Update () 
	{
		time ++;
        
		//if time limit reached bullet destroys itself
		if(time > timeLimit)
		{
			Destroy(gameObject);
		}
	}
}
