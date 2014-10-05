using UnityEngine;
using System.Collections;

public class MagicBall : Shot {
	public GameObject platName;
	public GameObject plat;
	bool aliveFlag;
	Material matWhite;
	Material matYellow;

	// Use this for initialization
	void Start () {
		matWhite = Resources.Load("Materials/MBWhite", typeof(Material)) as Material;
		matYellow = Resources.Load("Materials/MBYellow", typeof(Material)) as Material;
	}

	void TheStart(int dir){
		aliveFlag = true;
		facing = dir;
		health = maxHealth;

		//if(direction != 0)
		//	facing = direction;
		Vector2 impulse = new Vector2(2*baseSpeed*facing,-20);
		plat = (GameObject)Instantiate (plat,gameObject.transform.position,gameObject.transform.rotation);
		rigidbody.AddForce(impulse,ForceMode.Impulse);
		StartCoroutine("BallCool");
	}

	// Update is called once per frame
	void FixedUpdate () {
		touchDamage = health;
		StatusUpdate();
		Bounce();
		OffScreenBehavior();
	}

	//Function used to update entity status. Called from the fixed update of the child object
	public void StatusUpdate()
	{
		if(stun > 0) stun--;
		if(currentCooldown1 > 0) currentCooldown1--;
		if(currentCooldown2 > 0) currentCooldown2--;
		if(currentCooldown3 > 0) currentCooldown3--;
		if(currentCooldown4 > 0) currentCooldown4--;
		if(health < 1) Clear();
		//if(health < 1) Stub for destruction animation control
	}
	//Function used to control bounce physics. Called from the fixed update
	public void Bounce()
	{
		float finalSpeedX;
		float finalSpeedY;

		finalSpeedX = (baseSpeed * facing);// + environmentMoveX;
		//Debug.Log (finalSpeedX);

		if(yMove > falling)
		{
			yMove-=2;
		}
		finalSpeedY = yMove + environmentMoveY;
		
		movement = new Vector2(finalSpeedX,finalSpeedY);
		rigidbody.AddForce(movement);
		plat.transform.position = transform.position;
	}

	override public void Clear()
	{
		//TODO Insert animation here
		if(plat.transform.childCount > 0)
			plat.transform.GetChild(0).gameObject.SendMessage("EnvMoveClear");
		plat.transform.DetachChildren();
		Destroy (plat);
		Destroy(gameObject);
	}

	//Controls the shot's behavior when it hits something
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Standable")
		{
			terrainTouch = true;
			Vector2 norm = col.contacts[0].normal;
			Vector2 reflect = Vector3.Reflect(rigidbody.velocity, norm);
			reflect.Normalize();
			reflect*=baseSpeed;
			if(Mathf.RoundToInt(norm.x) != 0)
			{
				facing = (int)Mathf.RoundToInt(norm.x);
			}
			rigidbody.AddForce(reflect);
		}

		//Player layer object vs enemy layer object collisions
		if( col.gameObject.layer == 9)
		{
			// TODO make cleaner, inside enemy as message
			col.gameObject.GetComponent<Entity>().health -= gameObject.GetComponent<Entity>().touchDamage;
			col.gameObject.GetComponent<Entity>().stun = 20;

			gameObject.GetComponent<Entity>().health -= col.gameObject.GetComponent<Entity>().touchDamage;
			
			//If a shot hits anything other than a shot it zeros out it's health.  If a shot hits another shot the weaker one is destroyed
			if(col.gameObject.tag == "Shot")
			{
				if(gameObject.GetComponent<Entity>().health <= col.gameObject.GetComponent<Entity>().health)
					gameObject.GetComponent<Entity>().health = 0;
			}
			else
				gameObject.GetComponent<Entity>().health = 0;
		}
	}

	private IEnumerator BallCool(){
		while(aliveFlag){

			for(int i = 0; i < 8; ++i){
				yield return new WaitForSeconds(3/4f);
				gameObject.renderer.material = matWhite;
				yield return new WaitForSeconds(1/4f);
				gameObject.renderer.material = matYellow;
			}
			for(int i = 0; i < 4; ++i){
				yield return new WaitForSeconds(1/4f);
				gameObject.renderer.material = matWhite;
				yield return new WaitForSeconds(1/4f);
				gameObject.renderer.material = matYellow;
			}
			aliveFlag = false;
			Clear ();
		}
	}
}
