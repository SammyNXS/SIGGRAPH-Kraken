using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "Character"){
		//	col.rigidbody.useGravity = false;
		}
	}
	
	void OnCollisionExit(Collision col){
		if(col.gameObject.name == "Character"){
		//	col.rigidbody.useGravity = true;
		}
	}
}
