using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{

	public bool mainActionFlag, SecondaryActionFlag;
	public int cooldown = 50;
	public int currentCooldown = 0;
	public int energy = 100;
	public int currentEnergy;
	public int energyDrain = 0;

	Vector3 defaultPosition;

	// Use this for initialization
	public void WeaponStart () 
	{
		defaultPosition = transform.localPosition;
		currentEnergy = energy;
	}

	void FixedUpdate()
	{
		if(currentEnergy < energy) currentEnergy++;
		if(currentCooldown > 0) currentCooldown--;
	}

	public void AttackAnimation()
	{
		//Trigger animation built into weapon object
	}

	public void MainActionController(bool value)
	{
		mainActionFlag = value;
	}

	public void SecondaryActionController(bool value)
	{
		SecondaryActionFlag = value;
	}

	public Vector3 DefaultPosition 
	{
		get 
		{
			return defaultPosition;
		}
	}
}
