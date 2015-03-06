using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour 
{
	public Transform enemy;
	private int timer, limit;
	// Use this for initialization
	void Start () 
	{
		timer = 0;
		limit = 100;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer++;
		if(timer>limit)
		{
			timer = 0;
			Instantiate(enemy, transform.position, Quaternion.identity);
		}
	}
}
