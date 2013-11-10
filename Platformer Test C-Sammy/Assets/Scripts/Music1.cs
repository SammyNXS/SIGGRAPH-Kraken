using UnityEngine;
using System.Collections;

public class Music1 : MonoBehaviour {
	
	private static Music1 instance = null;
	
	
     
    public static Music1 Instance {
    get { return instance; }
    }
    void Awake() {
    if (instance != null && instance != this) {
    Destroy(this.gameObject);
    return;
    } else {
    instance = this;
    }
    DontDestroyOnLoad(this.gameObject);
    }
     
}
