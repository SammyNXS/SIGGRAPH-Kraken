using UnityEngine;
using System.Collections;

public class NewScene : MonoBehaviour {
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "Character"){
			if(Application.loadedLevelName == "PlatformerC_1"){
				Application.LoadLevel("InsideCastle");	
			}
			else{
				Application.LoadLevel("PlatformerC_1");	
			}
		}
	}
}
