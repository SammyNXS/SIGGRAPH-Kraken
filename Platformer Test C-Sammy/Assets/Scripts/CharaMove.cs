using UnityEngine;
using System.Collections;

public class CharaMove : MonoBehaviour {
	
	public int columns = 8;
	public int rows = 2;
	private Vector2 size; 
	private Vector2 offset; 
				
	public bool walkFlag;
	private bool isGrounded;
	
	private bool gunFlag;
	
	public int walk;
	public int standIndex;
	
	public int moveSpeed;
	
	public int jumpHeight;
	public float jumpBound;
	
	public Transform bullet;
	public Transform brick;

	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	
	public float xMove;
	
	public class Pair<T, U> {
	    public Pair() {
	    }
	
	    public Pair(T first, U second) {
	        this.First = first;
	        this.Second = second;
	    }
	
	    public T First { get; set; }
	    public U Second { get; set; }
	};
	
	//public GameObject[] bricks;
	public Pair<int, int>[] level;
	
	// Use this for initialization
	public void Start () {
		
		standIndex = 12;		
		size = new Vector2(1f/columns,1f/rows);
		setOffset (standIndex);
	
		renderer.material.SetTextureScale ("_MainTex", size);
		renderer.material.SetTextureOffset("_MainTex", offset);
		
		walkFlag=true;
		isGrounded = false;
		
		gunFlag = true;
		
		walk = 0;
		moveSpeed = 8;
		jumpHeight = 130;
		jumpBound = 1.8f;
	}
	
	// Update is called once per frame
	public void Update () {
		Move (); 
		Shoot();
		RaycastHit hit;
		Ray landRay = new Ray(transform.position, Vector3.down);
		//Debug.Log(transform.position.ToString());
		Debug.DrawRay(transform.position,Vector3.down*jumpBound, Color.green);

		//Debug.Log (Physics.Raycast (transform.position, -Vector3.up, 0.1f));
		if(!isGrounded){
			if(Physics.Raycast(landRay,out hit, jumpBound)){
				if(hit.collider.tag != "bullet"){
					isGrounded = true;	
				}
			}
		}
		if((Input.GetButtonDown("Jump") || Input.GetKey (KeyCode.Space)) && isGrounded){
			Jump();
		}
	}
	
	private void setOffset(int index){
		int columnIndex = index % columns;
		int rowIndex = index / columns;
		
		offset = new Vector2(size.x * columnIndex,1f-size.y-rowIndex*size.y);
	}
	
	private void Move(){
		
		int xAxis = ((Input.GetKey(right))?1:0)-((Input.GetKey(left))?1:0);
		xMove = xAxis*moveSpeed*Time.deltaTime;

		if((xMove != 0) && walkFlag){
			int multfactor = 1;
			int addFactor = 0;
			if(xMove < 0){
				multfactor = -1;
				addFactor = 1;
			}
			if(walk == 0){
				Debug.Log("stand");
				setOffset((standIndex+addFactor)*multfactor);
				++walk;
			}
			else if(walk == 1){
				Debug.Log("walk1");
				setOffset((standIndex+addFactor+1)*multfactor);
				++walk;
			}
			else if(walk == 2){
				Debug.Log("walk2");
				setOffset((standIndex+addFactor+2)*multfactor);
				++walk;
			}
			else if(walk == 3){
				Debug.Log("back21");				
				setOffset((standIndex+addFactor+1)*multfactor);
				walk = 0;
			}
			
			renderer.material.SetTextureOffset("_MainTex", offset);
			walkFlag=false;
			StartCoroutine("SpriteCool");
		}
		transform.
		transform.position+= new Vector3(xMove,0,0);
	}
	
	private void Shoot(){
		
		bool leftBool = Input.GetKey(KeyCode.LeftArrow);
		bool rightBool = Input.GetKey(KeyCode.RightArrow);
		
		if((leftBool || rightBool)  && gunFlag){	
			Transform b =  (Transform) Instantiate(bullet,transform.position,Quaternion.Euler(0,0,90));
			int dir = (rightBool ? 1:0)-(leftBool?1:0);
			b.gameObject.SendMessage("TheStart",dir);
			gunFlag = false;
			StartCoroutine("GunCool");
		}
	}
	
	void Jump(){
		isGrounded = false;
		rigidbody.AddForce(new Vector3(0,jumpHeight,0),ForceMode.Force);
	}
	
	private IEnumerator SpriteCool(){
		while(!walkFlag){
			yield return new WaitForSeconds(1f/7);
			walkFlag = true;
		}
	}
	
	private IEnumerator GunCool(){
		while(!gunFlag){
			Debug.Log("now");
			yield return new WaitForSeconds(1f/5);
			Debug.Log("shoot");
			gunFlag = true;
		}
	}
	
}
