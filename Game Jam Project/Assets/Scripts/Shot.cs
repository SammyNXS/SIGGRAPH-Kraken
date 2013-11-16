using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour 
{
	public GameObject PlayerBody;
	int shotDirection;
	// Use this for initialization
	void Start () 
	{
		shotDirection = Player.direction;
	}
	
	// Update is called once per frame
	void Update () 
	{
		rigidbody.AddForce(10*shotDirection,0,0);
		if(rigidbody.position.x < PlayerBody.transform.position.x-50 || rigidbody.position.x > PlayerBody.transform.position.x+50) Destroy (this.gameObject);
	}
}
