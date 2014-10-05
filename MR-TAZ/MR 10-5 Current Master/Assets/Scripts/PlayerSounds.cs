using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {

	float waitTime = 0.5f;
	bool running = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 && !running){
			StartCoroutine (footstep ());
		}
	}

	IEnumerator footstep() {
		running = true;
		audio.Play();
	    yield return new WaitForSeconds(waitTime);
		running = false;
	}

}
