using UnityEngine;
using System.Collections;

public class GenerateBullet : MonoBehaviour {
	
	public int moveSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	void TheStart (int dir) { // you can't use start. But this is just as good. 
	    moveSpeed= 20 * dir;
    }
	
    void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.gameObject.layer);
		if(collision.gameObject.layer == LayerMask.NameToLayer("Target")){
        	collision.gameObject.SendMessage ("ApplyDamage", SendMessageOptions.DontRequireReceiver);
        	Destroy(gameObject);
		}
    }
		
	// Update is called once per frame
	void Update () {
		transform.position+= new Vector3(moveSpeed*Time.deltaTime,0,0);
		if(!renderer.isVisible || moveSpeed == 0){
			Destroy(gameObject);	
		}
	}
}
