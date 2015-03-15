using UnityEngine;
using System.Collections;

public class FireWeaponScript : MonoBehaviour {

	public float Firecooldown = 0.1f;
	public int Maxbullets = 5;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Firecooldown > 0.0f)
			Firecooldown -= Time.deltaTime;
		
		if (Input.GetKey("space") && Firecooldown <= 0.0f && GameObject.FindGameObjectsWithTag("BULLET").Length < Maxbullets) {
			GameObject clone = (GameObject)Instantiate(bullet,this.transform.position + this.transform.up,this.transform.rotation);
			clone.rigidbody2D.velocity = this.transform.up / 0.1f;
			
			Firecooldown = 0.1f;
		}
	}
}
