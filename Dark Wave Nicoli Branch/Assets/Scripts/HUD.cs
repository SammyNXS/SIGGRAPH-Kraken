using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
	public GameObject player;
	private Architect architect;

	public GUIText playerHealth;
	public GUIText playerBuffs;
	public GUIText playerDebuffs;
	public GUIText currentWeapon;

	void Start()
	{
		architect = player.GetComponent<Architect>();
	}

	void OnGUI()
	{
		playerHealth.text = "Health: " + architect.health;
		playerBuffs.text = "Focus: " + architect.hasFocus + " Regen: " + architect.hasRegen;
		playerDebuffs.text = "Degen: " + architect.hasDegen;
		currentWeapon.text = "Weapon Slot: " + architect.weaponChoice;
	}
}
