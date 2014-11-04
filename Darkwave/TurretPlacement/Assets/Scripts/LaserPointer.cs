using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour 
{
    public Transform laser;
    private bool pointing;

	// Use this for initialization
	void Start () {
        pointing = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(pointing)
            Instantiate(laser, transform.position, transform.rotation);

        if(Input.GetKeyDown(KeyCode.R) && !pointing)
            pointing = true;
        else if(Input.GetKeyDown(KeyCode.R) && pointing)
            pointing = false;
	}
}
