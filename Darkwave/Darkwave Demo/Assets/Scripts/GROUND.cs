using UnityEngine;
using System.Collections;

public class GROUND : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Renderer>().material.SetColor ("_Color", UnityEngine.Color.blue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
