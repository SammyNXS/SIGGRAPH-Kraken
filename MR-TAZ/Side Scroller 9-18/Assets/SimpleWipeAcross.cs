using UnityEngine;
using System.Collections;

public class SimpleWipeAcross : MonoBehaviour {

	float moveSpeed;
	public int dir = -1;
	int count;
	public int max = 500;
	// Use this for initialization
	void Start () {
		moveSpeed = 0.4f;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(moveSpeed*dir*(new Vector3(1,0,0)));
		if(count == max)
		{
			dir = -dir;
			count = 0;
		}
		else{
			++count;
		}
	}
}
