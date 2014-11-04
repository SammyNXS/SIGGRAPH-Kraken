using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour 
{
	private int health;
	// Use this for initialization
	void Start () {
		health = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//moves enemy to the right
		transform.Translate(Vector3.forward * 4.0f * Time.deltaTime);
		//keeps the enemy moving in a straight line
		transform.rotation = Quaternion.identity;

		if(health <= 0)
			Destroy(gameObject);
	}

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.tag == "Shot")
			health-=5;
	}
}
