using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public float timer;
	public GameObject[] players;
	public int[] deaths;
	// Use this for initialization
	void Start () 
	{
		timer = 30f * 60;
		players = GameObject.FindGameObjectsWithTag("Player");
		deaths = new int[players.Length];
	}
	
	// Update is called once per frame
	void Update () 
	{
		int i=0;
		timer-=Time.deltaTime;
		for(i=0;i<players.Length;i++)
			if(players[i].GetComponent<Character>().health == 0)
				DeathController(i);
				
	}

	void DeathController(int player)
	{
		float deathTimer= deaths[player]++*15f;
		while(deathTimer > 0)
			deathTimer-=Time.deltaTime;
		players[player].transform.position = this.transform.position;
		players[player].GetComponent<Character>().health = players[player].GetComponent<Character>().maxHealth;

	}
}