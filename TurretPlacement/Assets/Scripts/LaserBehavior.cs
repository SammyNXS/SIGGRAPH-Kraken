using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        rigidbody.AddForce(transform.forward * 1000);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
