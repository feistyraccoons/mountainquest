using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour 
{
	public 		GameObject objectToSpawn;
	public 		float spawnCooldown = 5.0f;
	public 		float xRange = 4;
	public 		float yRange = 0;
	public 		float zRange = 4;
	
	private		float nextSpawn = 0;
	
	void Start()
	{
		nextSpawn = Random.value * spawnCooldown;
	}
	
	void Update () 
	{
		if(Time.time > nextSpawn && objectToSpawn != null)
		{	
			SpawnObject();
		}			
	}
	
	void SpawnObject()
	{
		// Set the position to spawn at
		Vector3 spawnPos = transform.position;

		spawnPos.x += Random.value * xRange - xRange/2.0f; // Mathf.Sin(Mathf.Deg2Rad * Random.value * 360) * xRange;
		spawnPos.y += Random.value * yRange - yRange/2.0f; // Mathf.Cos(Mathf.Deg2Rad * Random.value * 360) * yRange;
		spawnPos.z += Random.value * zRange - zRange/2.0f; // Mathf.Sin(Mathf.Deg2Rad * Random.value * 360) * zRange;
		
		// Instantiate the Object
		Instantiate(objectToSpawn, spawnPos, Quaternion.identity); //Quaternion.LookRotation(Random.onUnitSphere));
		
		// Set the spawn timer
		nextSpawn = Time.time + spawnCooldown;		
	}
}
