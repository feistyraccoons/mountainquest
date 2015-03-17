using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
	public float m_fRange = 1.0f,resetTimer=0.5f;
	private float timer=0;

	void Start(){
		timer = resetTimer;
	}


	public override void Update(){
			timer -= Time.deltaTime;

		if (timer <= 0) {
			GetComponent<BoxCollider2D>().enabled = true;
		}

	}

	void OnTriggerStay2D(Collider2D other) {

//		if(other.gameObject != transform.parent.gameObject)
		if (other.tag == "Player") {
			other.SendMessage ("takeDamage", m_fDamage);
			GetComponent<BoxCollider2D>().enabled = false;
			timer=resetTimer;
//				UnityEditor.PrefabUtility.ResetToPrefabState(this);
		}
	}
}

