using UnityEngine;
using System.Collections;

public class BossMovement : Movement {

	//0 = idle
	//1 = patrol
	//2 = seek
	public int currMovement = 0;

	//if false -> melee
	//if true -> ranged
	public bool isCurrRanged = false;

	public float aggrotimer = 0.0f;

	//10-7.5 patrol
	//7.5-5 idle
	//5-2.5 patrol
	//2.5-0 idle and reset
	public float masterTimer = 10.0f;

	public float attackChangeTimer = 3.0f;

	public GameObject target = null;

	public bool wallhit = false;

	// Use this for initialization
	public override void Start () {
		speed = 2.0f;
		trueup = this.transform.up;
	}
	
	// Update is called once per frame
	public override void Update () {
		masterTimer -= Time.deltaTime;

		if (ground != null) {
			if (aggrotimer <= 0.0f) {

				if (masterTimer >= 7.5f || (masterTimer < 5.0f && masterTimer >= 2.5f)) {
					//patrol
					rigidbody2D.velocity = new Vector2(speed,0);
					
					if(this.collider2D.bounds.min.x + rigidbody2D.velocity.x * Time.deltaTime < ground.collider2D.bounds.min.x)
						dir = true;
					else if (this.collider2D.bounds.max.x + rigidbody2D.velocity.x * Time.deltaTime > ground.collider2D.bounds.max.x)
						dir = false;
					
					if (!dir)
						rigidbody2D.velocity*=-1;
				}
				//else idle

				if(masterTimer <= 0.0f)
					masterTimer = 10.0f;

			} else {

				if(!isCurrRanged){
					aggrotimer-=Time.deltaTime;
					if(target!=null)
					//normal seek paired to melee attack
					rigidbody2D.velocity = new Vector2(
						((target.transform.position - this.transform.position).normalized.x
					 /Mathf.Abs((target.transform.position - this.transform.position).normalized.x)*speed),0);
					
					if((this.collider2D.bounds.min.x + rigidbody2D.velocity.x * Time.deltaTime < ground.collider2D.bounds.min.x && rigidbody2D.velocity.x < 0) ||
					   (this.collider2D.bounds.max.x + rigidbody2D.velocity.x * Time.deltaTime > ground.collider2D.bounds.max.x && rigidbody2D.velocity.x > 0) ||
					   wallhit)
						rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
					
					wallhit = false;
				}else{
					//modified seek paired to ranged attack
					rigidbody2D.velocity = new Vector2(
						-((target.transform.position - this.transform.position).normalized.x
					 /Mathf.Abs((target.transform.position - this.transform.position).normalized.x)*speed),0);
					
					if((this.collider2D.bounds.min.x + rigidbody2D.velocity.x * Time.deltaTime < ground.collider2D.bounds.min.x && rigidbody2D.velocity.x < 0) ||
					   (this.collider2D.bounds.max.x + rigidbody2D.velocity.x * Time.deltaTime > ground.collider2D.bounds.max.x && rigidbody2D.velocity.x > 0) ||
					   wallhit)
						rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
				}

				attackChangeTimer -= Time.deltaTime;

				if(Random.Range(1,18) > 15 && attackChangeTimer <= 0.0f){
					isCurrRanged = !isCurrRanged;
					attackChangeTimer = 3.0f;
				}

			}

		}
	}

	public override void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Platform") {
			ground = coll.gameObject;
			this.transform.up = trueup;
		} else if (coll.gameObject.tag == "Wall") {
			wallhit = true;
		} else {
			transform.position -= (Vector3)rigidbody2D.velocity;
			dir = !dir;
		}
	}
	
	public void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform") {
			ground = null;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			target = coll.gameObject;
			aggrotimer = 3.0f;
		}
	}
	
	void OnTriggerStay2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			target = coll.gameObject;
			aggrotimer = 3.0f;
		}
	}
}
