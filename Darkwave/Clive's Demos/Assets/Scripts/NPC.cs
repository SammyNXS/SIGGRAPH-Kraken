using UnityEngine;
using System.Collections;

public class NPC : Entity 
{
	//Behavior variables (set in editor)
	public int behavior;
	public int patrolSize;
	public int sensorRange;
	public int engagementRange;
	public bool inSight;
	public int interest;
	public int attentionSpan;
	public int distance;
	public GameObject target;


	public Vector2 startPosition;

	//Movement
	public float jumpHeight;// set in editor
	public float falling = -20;
	public Vector3 direction;
	public bool isJumping;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
