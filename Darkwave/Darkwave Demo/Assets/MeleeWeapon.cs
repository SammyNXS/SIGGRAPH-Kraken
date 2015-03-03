﻿using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon 
{

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
					//Weapon swing stub
			}
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
