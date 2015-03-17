using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
	public float m_fRange = 1.0f;
	private float timer = 1.5f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
			Destroy (gameObject);
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") 
			other.SendMessage ("takeDamage", m_fDamage);
	}
}

