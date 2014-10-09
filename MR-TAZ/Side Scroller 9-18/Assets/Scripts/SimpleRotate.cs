using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

	int rotateSpeed;
	// Use this for initialization
	void Start () {
		rotateSpeed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,Time.deltaTime*rotateSpeed);
	}
}
