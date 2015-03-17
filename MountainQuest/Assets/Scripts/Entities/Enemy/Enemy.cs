using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class Enemy : Entity
{
	// Movement
	public enum MovementTypes
	{
		Idle,
		Patrol,
		Wander,
		Seek
	}
	private MovementTypes defaultMovement;
	public MovementTypes activeMovement = MovementTypes.Idle;
	private Movement movement; 

	// Attack
	public float changeRangedAttackEvery = 0;
	private float changeAttackTimer = 3 ;
	// Melee
	public 	MeleeAttack meleeAttack;
	// Ranged
	public  RangedAttack rangedAttack;

	// Aggro
	private bool aggro = false;
	public CircleCollider2D safeArea;

	// Use this for initialization
	public override void Start ()
	{
//		if (meleeAttack != null)
//			meleeAttack.sword = meleeAttack.theWeapons [0] as Sword;
		if (rangedAttack != null)
			rangedAttack.projectile = rangedAttack.theWeapons [0] as Projectile;

		defaultMovement = activeMovement;

		this.gameObject.AddComponent<PatrolMovement> ().enabled = false;
		this.gameObject.AddComponent<WanderMovement> ().enabled = false;
		this.gameObject.AddComponent<SeekMovement> ().enabled = false;
		this.gameObject.AddComponent<IdleMovement> ().enabled = false;

		movement = GetComponent<IdleMovement> ();
		changeMovement (activeMovement);
		base.Start ();
	}
	
	
	// Update is called once per frame
	public override void Update ()
	{
		if (aggro) {
			if (!(activeMovement == MovementTypes.Seek))
				changeMovement (MovementTypes.Seek);


			if (meleeAttack != null) {

			if (meleeAttack.theWeapons.Count > 1) 
				meleeAttack.sword = meleeAttack.theWeapons [Random.Range (0, meleeAttack.theWeapons.Count)] as Sword;

				meleeAttack.swing();
			} else if (rangedAttack != null) {

				if (rangedAttack.theWeapons.Count > 1) {
					changeAttackTimer -= Time.deltaTime;
						
					if (changeAttackTimer < 0.0f) {
						rangedAttack.projectile = rangedAttack.theWeapons [Random.Range (0, rangedAttack.theWeapons.Count)] as Projectile;
						changeAttackTimer = changeRangedAttackEvery;
					}
				}
				rangedAttack.enabled = true;
			} 



		} else {
//			if (meleeAttack != null && !attacking)
//				meleeAttack.enabled = false;
//			if (rangedAttack != null )
//				rangedAttack.enabled = false;
			if (activeMovement != defaultMovement)
				changeMovement (defaultMovement);
		}	

		base.Update ();
	}

	void changeMovement (MovementTypes newMovement)
	{
		movement.enabled = false;
		activeMovement = newMovement;
		switch (activeMovement) {
		case MovementTypes.Patrol:
			movement = this.gameObject.GetComponent<PatrolMovement> ();
			break;
		case MovementTypes.Wander:
			movement = this.gameObject.GetComponent<WanderMovement> ();
			break;
		case MovementTypes.Seek:
			movement = this.gameObject.GetComponent<SeekMovement> ();
			break;
		default:
			movement = this.gameObject.GetComponent<IdleMovement> ();
			break;
		}
		movement.enabled = true;

	}
	 
//	bool inMeleeRange ()
//	{
//		bool inRange = false;
//		GameObject player = GameObject.FindGameObjectWithTag ("Player");
//		if (player != null) {
//
//			RaycastHit2D hit;
//			
//			if (Vector3.Distance (transform.position, player.transform.position) < meleeAttack.sword.m_fRange) {
//				Debug.DrawRay (transform.position, (player.rigidbody2D.transform.position - rigidbody2D.transform.position), Color.red);
//				hit = Physics2D.Raycast (rigidbody2D.transform.position, (player.rigidbody2D.transform.position - rigidbody2D.transform.position), meleeAttack.sword.m_fRange);
//				if (hit.collider != null) {
//					inRange = true;
//				}
//			}
//
//		}
//		return inRange;
//	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
//			if (other.GetComponent<BoxCollider2D> ()) 
				aggro = true;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
//			if (other.GetComponent<BoxCollider2D> ()) 
				aggro = false;
		}
	}
	
//	void OnCollisionEnter2D (Collision2D coll)
//	{
//		Weapon proj = coll.gameObject.GetComponent<Weapon> ();
//		if (proj != null) {
//			aggro = true;
//			health.takeDamage (proj.m_fDamage);
//		}
//	}
	
}
