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

	//Attack
	private float changeAttackTimer = 0.0f;
	public int changeMeleeAttackEvery = 5, changeRangedAttackEvery = 5;
	
	// Melee
	public 	MeleeAttack meleeAttack;
	// Ranged
	public  RangedAttack rangedAttack;

	// Aggro
	private bool aggro = false;
	public CircleCollider2D safeArea;

	// Use this for initialization
	void Start ()
	{

		defaultMovement = activeMovement;

		this.gameObject.AddComponent<PatrolMovement> ().enabled = false;
		this.gameObject.AddComponent<WanderMovement> ().enabled = false;
		this.gameObject.AddComponent<SeekMovement> ().enabled = false;
		this.gameObject.AddComponent<IdleMovement> ().enabled = false;

		movement = GetComponent<IdleMovement> ();
		changeMovement (activeMovement);

	}
	
	
	// Update is called once per frame
	public override void Update ()
	{
		if (aggro) {
			if (!(activeMovement == MovementTypes.Seek))
				changeMovement (MovementTypes.Seek);


			if (meleeAttack != null) {
				if (inMeleeRange ()) {

					if (availableSwords.Count > 1) {
						changeAttackTimer -= Time.deltaTime;
						if (changeAttackTimer < 0.0f) {
							meleeAttack.sword = availableSwords [availableSwords.BinarySearch (meleeAttack.sword) + 1];
							changeAttackTimer = changeMeleeAttackEvery;
						}
					}
					meleeAttack.enabled = true;
				} else
					meleeAttack.enabled = false;
					
			} else if (rangedAttack != null) {

				if (rangedAttack.theWeapons.Count > 1) {
					changeAttackTimer -= Time.deltaTime;
						
					if (changeAttackTimer < 0.0f) {
						rangedAttack.projectile = availableProjectiles [Random.Range (0, availableProjectiles.Count)];
						changeAttackTimer = changeRangedAttackEvery;
					}
				}
//				rangedAttack.Update ();
				rangedAttack.enabled = true;
			} 



		} else {
			if (meleeAttack != null)
				meleeAttack.enabled = false;
			if (rangedAttack != null)
				rangedAttack.enabled = false;
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
	 
	bool inMeleeRange ()
	{
		availableSwords.Clear ();

		RaycastHit2D hit = Physics2D.Raycast (transform.position,-Vector2.right,10.0f);
		if (hit.collider != null && hit.collider.gameObject.tag == "Player") {

			Debug.Log("Ray collided: "+hit.collider.gameObject);
			float distance1 = Mathf.Abs (hit.point.x - transform.position.x);
			float distance2 = Vector2.Distance (hit.point, transform.position);

			if (distance1 != distance2) 
				Debug.LogWarning ("Distances are different (" + distance1 + " and " + distance2 + ")");

			bool cascade = false;
			for (int i = meleeAttack.theWeapons.Count-1; i >= 0; i--) {
				if (cascade || distance1 <= (meleeAttack.theWeapons [i] as Sword).range) {
					availableSwords.Add (meleeAttack.theWeapons [i] as Sword);
					cascade = true;
				}
			}
		}

		return availableSwords.Count > 0;
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			
			if (other.GetComponent<BoxCollider2D> ()) {
				aggro = true;
				Debug.Log ("Collided with " + other.gameObject);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			if (other.GetComponent<BoxCollider2D> ()) 
				aggro = false;
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		Weapon proj = coll.gameObject.GetComponent<Weapon> ();
		if (proj != null) {
			aggro = true;
			health.takeDamage (proj.m_fDamage);
		}
	}
	
}
