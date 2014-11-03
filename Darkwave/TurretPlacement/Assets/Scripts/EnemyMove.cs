using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//moves enemy to the right
		transform.Translate(Vector3.right * 2.0f * Time.deltaTime);
	}
}
