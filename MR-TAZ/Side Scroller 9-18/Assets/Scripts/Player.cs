using UnityEngine;
using System.Collections;

/*Controls all aspects of the player object
*/
public class Player : Entity 
{
	int loopNum = 1;
	int attackLoop = 1;
	bool attackFlag = false;
	bool attacking = true;

	int attack = 0;
	int attackIndex = 5;

	void Start()
	{
		standIndex = 1;		
		size = new Vector2(1f/columns,1f/rows);
		setOffset (standIndex);

		walkFlag=true;
		walk = 0;
		
		renderer.material.SetTextureScale ("_MainTex", size);
		renderer.material.SetTextureOffset("_MainTex", offset);
	}

	// FixedUpdate is called at a regular interval to activate the controller functions and update the status variables.
		// Status variables are in the entity class
	void FixedUpdate () 
	{
		PlayerMoveController();
		PlayerAttackController();
		StatusUpdate();
	}

	// Reads player input and changes movement variables accordingly
	void PlayerMoveController()
	{
		if(Input.GetAxis("Horizontal") < -0.1)
			direction = -1;
		else if(Input.GetAxis("Horizontal") > 0.1)
			direction = 1;
		else
			direction = 0;

		if(Input.GetKey(KeyCode.Space))
		{
		   isJumping = true;
		   transform.parent = null;
		}
		else
			isJumping = false;
		Movement();
	}

	protected override void AnimationController()
	{
		//TODO Add stand animation

		//TODO Add spellcast animation
		if(attackFlag && attacking)
		{
			int addFactor = 0;

			//0,1,2,1,0
			//6,7,8,7,6
			// Custom switch case to loop through walking offsets
			// for sprite sheet
			switch(attack)
			{
			case 0:
				//Debug.Log("1: shoots");
				switch(attackLoop)
				{
				case 1:
					//Debug.Log("1: shoots");
					++attack;
					break;
				case 2:
					//Debug.Log("1: ends");
					--attackLoop;
					attackFlag = false;
					break;
				default:
					break;
				}
				break;
			case 1:
				switch(attackLoop)
				{
				case 1:
					//Debug.Log("2: case 1");
					++attackLoop;
					++attack;
					break;
				case 2:
					//Debug.Log("2: case 2");
					--attack;
					break;
				default:
					break;
				}
				break;
			case 2:
				//Debug.Log("3: peak");
				--attack;
				break;
			default:
				break;
			}
			
			if(facing < 0){
				addFactor = 6-attack;
			}
			else{
				addFactor += attack;
			}
			setOffset(attackIndex+addFactor);
			renderer.material.SetTextureOffset("_MainTex", offset);
			attacking = false;
			StartCoroutine("SpellCool");
		}
		if(Input.GetAxis("Horizontal") != 0 && walkFlag)
		{
			int addFactor = 0;
			// Custom switch case to loop through walking offsets
			// for sprite sheet



			//0,1,2,1,0,3
			switch(walk)
			{
			case 0:
				switch(loopNum)
				{
				case 1:
					++walk;
					break;
				case 2:
					loopNum = 1;
					walk += 3;
					break;
				default:
					break;
				}
				break;
			case 1:
				switch(loopNum)
				{
				case 1:
					++loopNum;
					++walk;
					break;
				//move to 1
				case 2:
					--walk;
					break;
				default:
					break;
				}
				break;
			case 2:
				--walk;
				break;
			case 3:
				walk -= 3;
				break;
			default:
				break;
			}

			if(Input.GetAxis("Horizontal") < 0){
				addFactor = 14-walk;
			}
			else{
				addFactor += walk;
			}

			setOffset(standIndex+addFactor);

			renderer.material.SetTextureOffset("_MainTex", offset);
			walkFlag = false;
			StartCoroutine("SpriteCool");
		}
	}

	void EnvMoveClear(){
		transform.parent = null;
		terrainTouch = false;
		yMove = 0;
		environmentMoveX = 0;
		environmentMoveY = 0;
	}

	//Reads player input and changes attack variables accordingly.Shot attack function is in the entity class
	void PlayerAttackController()
	{
		if(Input.GetKey(KeyCode.Alpha1))
		{
			shotAttackType = 1;

			shotSpawnPosition = transform.position;
			shotSpawnRotation = Quaternion.Euler(0,facing==1?0:180,0);

			ShotAttack();
		}
		else if(Input.GetKey(KeyCode.Alpha2))
		{
			shotAttackType = 2;

			shotSpawnPosition = transform.position;
			shotSpawnRotation = Quaternion.Euler(0,facing==1?0:180,0);

			ShotAttack();
		}
		else if(Input.GetKey(KeyCode.Alpha3))
		{
			shotAttackType = 3;

			shotSpawnPosition = transform.position+(new Vector3(4*facing,4,0));
			shotSpawnRotation = Quaternion.Euler(0,facing==1?0:180,0);
			
			ShotAttack();
		}
		else
			shotAttackType = 0;
		if(shotAttackType != 0)
		{
			attackFlag = true;
		}
	}
	//Controls reactions to collisions
	void OnCollisionEnter(Collision col)
	{
		if((stun == 0) && (col.gameObject.layer == 9))
		{
			gameObject.GetComponent<Entity>().health -= col.gameObject.GetComponent<Entity>().touchDamage;
			stun = 20;
		}
		if(col.gameObject.tag == "Door")
		{
			CharacterSpawner.destination = col.gameObject.GetComponent<Door>().doorDestination;
			CharacterSpawner.checkpoint = col.gameObject.GetComponent<Door>().doorPosition;
			CharacterSpawner.levelChange = true;
		}
	}
	
	private IEnumerator SpriteCool(){
		while(!walkFlag){
			yield return new WaitForSeconds(1f/7);
			walkFlag = true;
		}
	}
	private IEnumerator SpellCool(){
		while(!attacking){
			yield return new WaitForSeconds(1/7f);
			attacking = true;
		}
	}
}