using UnityEngine;
using System.Collections;

public class MovingPanel : MonoBehaviour 
{
	public int xMoveStart, xMoveEnd, xMoveSpeed, yMoveStart, yMoveEnd, yMoveSpeed;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(transform.position.x < xMoveStart || transform.position.x > xMoveEnd) xMoveSpeed *= -1;
		if(transform.position.y < yMoveStart || transform.position.y > yMoveEnd) yMoveSpeed *= -1;
		transform.Translate(new Vector2(xMoveSpeed*Time.deltaTime, yMoveSpeed*Time.deltaTime));
	}

}
