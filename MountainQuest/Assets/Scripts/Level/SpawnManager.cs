using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GameObject SpawnMe;
	public Enemy[] spawnedEnemies;
	public int maxSpawns = 20,spawnEveryXSeconds=3;
	private int numSpawned=0;
	private float spawnTimer;

	// Use this for initialization
	void Start () {
		numSpawned = 0;
		spawnTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;

		if (spawnTimer <= 0.0f && numSpawned < maxSpawns) {
			spawnedEnemies[numSpawned] = (Enemy)Instantiate (SpawnMe);
			spawnedEnemies[numSpawned].activeMovement = Enemy.MovementTypes.Wander;
			numSpawned++;
			spawnTimer = spawnEveryXSeconds;
		}
	}

	void despawn(){
		numSpawned--;
	}
}
