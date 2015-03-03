﻿using UnityEngine;
using System.Collections;

public class RangedWeapon : Weapon 
{
	public int secondaryActionType; //0 Zoom, 1 Secondary Attack
	public float accuracy;
	public bool hasScope;
	public GameObject shot;

	// Use this for initialization
	void Start ()
	{
		WeaponStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		MainAction();
		SecondaryAction();
	}

	public void MainAction()
	{
		if(mainActionFlag)
		{
			AttackAnimation();
			if(currentCooldown == 0)
			{
				//Shot spawn position temporarily changed until correct model can be imported
				//Vector3 shotSpawnPosition = gameObject.transform.position;
				Vector3 shotSpawnPosition = new Vector3(
					gameObject.transform.position.x, 
					gameObject.transform.position.y+.4f,
					gameObject.transform.position.z);

				Vector3 bulletSpread = new Vector3(Random.Range(-1f,1f)*(10-accuracy),Random.Range(-1f,1f)*(10-accuracy),0);
				Quaternion shotSpawnRotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + bulletSpread);
				
				Instantiate(shot, shotSpawnPosition, shotSpawnRotation);
				currentCooldown = cooldown;
				energy -= energyDrain;
			}
		}
	}
	
	public void SecondaryAction()
	{
		if(SecondaryActionFlag)
		{
			gameObject.transform.localPosition = new Vector3(0,-0.7f,0);
			if(hasScope)
				;
		}
		else
		{
			gameObject.transform.localPosition = defaultPosition;
		}
	}
}
