using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttack : Attack {
	[HideInInspector]
	public Projectile projectile;
	public float timer =3;

	void Start () {
		theWeapons = new List<Weapon> ();
	}
	
	public override void Update () {
		attackTimer-=Time.deltaTime;
		if (attackTimer <= 0) {

			attackTimer= timer;
			Vector3 fireDir;
			if(this.CompareTag("Player"))
				fireDir = Input.mousePosition - this.transform.position;
			else{
				if(GameObject.FindGameObjectWithTag("Player") != null)
				fireDir = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
			}
				fireDir.Normalize();


			Projectile newProjectile = (Projectile)Instantiate(projectile);
			newProjectile.transform.position = this.transform.position + fireDir*2;
			float angle = Vector3.Angle(Vector3.right, fireDir);
			if(fireDir.y < 0)
				angle = -angle;
			newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
			newProjectile.GetComponent<Projectile>().Shoot(fireDir);

		}
	}
}
