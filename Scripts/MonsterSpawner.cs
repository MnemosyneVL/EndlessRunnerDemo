using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
    //This class randomly spawns monsters always leaving a way for player
	//Spawners
	[SerializeField]
	GameObject[] spawners = new GameObject[5];
	private bool[] spawnerIsOccupied = new bool[5];
	//Monsters
	[SerializeField]
	GameObject[] monsters = new GameObject[3];
	[SerializeField]
	GameObject lightning;
	//General
	private float lightningSpawnCoolDown;
	private float spawnCoolDown;
	private float initialSpawnCoolDown;
	private int amountOfMonsters;
	private int monsterType;
	private int spawnerPick;
	private int spawnerPickLightning;
	GameObject speedManager;
	private float speedVariable = 4;


	// Use this for initialization
	void Start () {
		speedManager = GameObject.FindGameObjectWithTag("Manager");
		speedVariable = speedManager.GetComponent<SpeedController>().speed;
		spawnCoolDown  = Random.Range(12/speedVariable,28/speedVariable);
		initialSpawnCoolDown = spawnCoolDown;
		lightningSpawnCoolDown = 4f;
		amountOfMonsters = Random.Range(2,5);
		for (int i = 0; i < 5 ; i++)
			{
				spawnerIsOccupied[i] = false;
			}
	}
	
	// Update is called once per frame
	void Update () {
		speedVariable = speedManager.GetComponent<SpeedController>().speed;
		if (spawnCoolDown > 0)
		{
			spawnCoolDown -= Time.deltaTime;
		}
		else
		{
			for (int i = 0; i < amountOfMonsters ; i++)
			{
				spawnerPick = Random.Range(0,5);
				while (spawnerIsOccupied[spawnerPick] == true)
					{
						spawnerPick = Random.Range(0,5);
					}
				monsterType = Random.Range(0,3);
				Instantiate(monsters[monsterType],spawners[spawnerPick].transform.position,Quaternion.identity);
				spawnerIsOccupied[spawnerPick] = true;
			} 
			for (int i = 0; i < 5 ; i++)
			{
				spawnerIsOccupied[i] = false;
			}
			spawnCoolDown = Random.Range(12/speedVariable,24/speedVariable);
			initialSpawnCoolDown = spawnCoolDown;
			//spawnCoolDown = 5;
			amountOfMonsters = Random.Range(2,5);
		}
		if (lightningSpawnCoolDown > 0)
		{
			lightningSpawnCoolDown -= Time.deltaTime;
		}
		else
		{
			if(initialSpawnCoolDown - spawnCoolDown < 4/speedVariable || spawnCoolDown < 4/speedVariable)
			{
				lightningSpawnCoolDown += 4/speedVariable;
			}
			else
			{
				spawnerPickLightning = Random.Range(0,5);
				Instantiate(lightning,spawners[spawnerPickLightning].transform.position,Quaternion.identity);
				lightningSpawnCoolDown = 4f;
			}
		}

	}
}
