using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon 
{
	Quaternion defaultRotation;
	// Use this for initialization
	void Start () 
	{
		WeaponStart();
		defaultRotation = transform.localRotation;
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
				gameObject.transform.RotateAround(gameObject.transform.parent.position, Vector3.up, 20f);
				currentCooldown=cooldown;
			}
		}
		else 
		{
			gameObject.transform.localPosition = DefaultPosition;
			gameObject.transform.localRotation = defaultRotation;
		}
	}
	
	public void SecondaryAction()
	{
		if(SecondaryActionFlag)
		{
			if(!mainActionFlag)
			{

				if(currentCooldown == 0)
				{
					AttackAnimation();
					//Weapon swing stub

					currentCooldown = cooldown;
					energy -= energyDrain;
				}
			}
		}
	}
}

