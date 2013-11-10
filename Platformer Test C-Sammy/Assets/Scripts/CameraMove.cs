using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	private Transform player;
	
	private int leftBound = -19;
	private int rightBound = 14;
	
	public float dampTime = 0.4f;
	public float defaultY;
	private Vector3 velocity = Vector3.zero;
	
	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Vector3 point = camera.WorldToViewportPoint(player.position);
		defaultY = (player.position-camera.ViewportToWorldPoint(new Vector3(0.5f,0.2f,point.z))).y;
		switch(Application.loadedLevelName){
		case "InsideCastle":
			leftBound = -80;
			rightBound = 88;
			break;	
		default:
			break;
		}
	}
	
	void Update(){
			Vector3 point = camera.WorldToViewportPoint(player.position);
			Vector3 delta = player.position-camera.ViewportToWorldPoint(new Vector3(0.5f,0.2f,point.z));
			//delta += new Vector3(0,-delta.y,0);
		if(player.position.x > leftBound && player.position.x < rightBound){ 
			Vector3 destination = transform.position+delta;
			transform.position = Vector3.SmoothDamp(transform.position,destination,ref velocity,dampTime);
		}
		else{
			delta += new Vector3(-delta.x,0,-delta.z);
			Vector3 destination = transform.position+delta;
			transform.position = Vector3.SmoothDamp(transform.position,destination,ref velocity,dampTime);
		}
	}

}
