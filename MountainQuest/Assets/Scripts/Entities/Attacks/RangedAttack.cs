using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttack : Attack {
	[HideInInspector]
	public Projectile projectile;
	
	// Use this for initialization
	void Start () {
		theWeapons = new List<Weapon> ();
	}
	
	// Update is called once per frame
	public override void Update () {
		attackTimer-=Time.deltaTime;
		if (attackTimer <= 0) {
			Vector3 fireDir;
			if(this.CompareTag("Player"))
				fireDir = Input.mousePosition - this.transform.position;
			else
				fireDir = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
			fireDir.Normalize();

//			float dot = Vector2.Dot(fireDir, Vector2.right);
//			if(dot >= Mathf.Cos(Mathf.PI / 8)){
//				fireDir.Set(1, 0);
//			}
//			else if(dot >= Mathf.Cos(3 * Mathf.PI / 8)){
//				if(fireDir.y > 0)
//					fireDir.Set(1, 1);
//				else
//					fireDir.Set(1, -1);
//			}
//			else if(dot >= -Mathf.Cos(3 * Mathf.PI / 8)){
//				if(fireDir.y > 0)
//					fireDir.Set(0, 1);
//				else
//					fireDir.Set(0, -1);
//			}
//			else if(dot >= -Mathf.Cos(Mathf.PI / 8)){
//				if(fireDir.y > 0)
//					fireDir.Set(-1, 1);
//				else
//					fireDir.Set(-1, -1);
//			}
//			else{
//				fireDir.Set(-1, 0);
//			}
//			fireDir.Normalize();

			GameObject newProjectile = (GameObject)Instantiate(projectile);
			newProjectile.transform.position = this.transform.position + fireDir/3;
			float angle = Vector3.Angle(Vector3.right, fireDir);
			if(fireDir.y < 0)
				angle = -angle;
			newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
			newProjectile.GetComponent<Projectile>().Shoot(fireDir);

			attackTimer = attackSpeed;
		}
	}
}
