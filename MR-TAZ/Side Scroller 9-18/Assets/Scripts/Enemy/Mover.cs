using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float baseSpeed;
	public float jumpHeight;
	public float falling = -20;
	public int direction;
	public bool isJumping;

	int facing = 1;
	bool terrainTouch = false;
	float yMove = 0;
	float environmentMoveX = 0;
	float environmentMoveY = 0;
	Vector2 movement;
	
	public void Movement()
	{
		float finalSpeedX;
		float finalSpeedY;
		
		if(direction != 0)
			facing = direction;
		
		finalSpeedX = (baseSpeed * direction) + environmentMoveX;
		
		if(terrainTouch)
			if(isJumping)
				yMove = jumpHeight;
			else
			yMove = 0;
		else if(yMove > falling)
			yMove--;
		
		finalSpeedY = yMove + environmentMoveY;
		
		movement = new Vector2(finalSpeedX * Time.deltaTime, finalSpeedY * Time.deltaTime);
		
		transform.Translate(movement);
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Terrain")
			terrainTouch = true;
		if(col.gameObject.tag == "Moving")
		{
			terrainTouch = true;
			environmentMoveX = col.gameObject.GetComponent<MovingPanel>().xMoveSpeed;
			environmentMoveY = col.gameObject.GetComponent<MovingPanel>().yMoveSpeed;
		}
		if((gameObject.tag == "Player" && col.gameObject.tag == "Enemy") || (gameObject.tag == "Enemy" && col.gameObject.tag == "Player"))
			this.gameObject.GetComponent<Entity>().health -= col.gameObject.GetComponent<Entity>().touchDamage;
	}
	
	void OnCollisionExit(Collision col)
	{
		terrainTouch = false;
		environmentMoveX = 0;
		environmentMoveY = 0;
	}
}
