using UnityEngine;
using System.Collections;

public class BreakableBrick : MonoBehaviour {
	Material mat2 = Resources.Load("Brick2hp", typeof(Material)) as Material;
	Material mat1 = Resources.Load("Brick1hp", typeof(Material)) as Material;

	
	public int hp;
	
	// Use this for initialization
	void Start () {
		hp = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if(hp == 2){
			gameObject.renderer.material = mat2;
		}
		if(hp == 1){
			gameObject.renderer.material = mat1;
		}
		if(hp == 0){
			Destroy (this.transform.gameObject);
		}
	}
	
	void ApplyDamage(){
		--hp;
	}
}
