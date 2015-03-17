using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttack : Attack {
	[HideInInspector]
	public Projectile projectile;
	
	void Start () {
		theWeapons = new List<Weapon> ();
	}
	
	public override void Update () {
		attackTimer-=Time.deltaTime;
		if (attackTimer <= 0) {
			Vector3 fireDir;
			if(this.CompareTag("Player"))
				fireDir = Input.mousePosition - this.transform.position;
			else
				fireDir = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
			fireDir.Normalize();


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
