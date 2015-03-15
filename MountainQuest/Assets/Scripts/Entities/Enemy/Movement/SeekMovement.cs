using UnityEngine;
using System.Collections;

public class SeekMovement : Movement {

	public GameObject target = null;
	public float aggrotimer = 0.0f;
	public bool wallhit = false;

	// Use this for initialization
	public override void Start () {
		speed = 2.0f;
		trueup = this.transform.up;
	}
	
	// Update is called once per frame
	public override void Update () {

		aggrotimer -= Time.deltaTime;

		if (aggrotimer <= 0.0f)
			target = null;

		if (target != null && ground != null) {
			rigidbody2D.velocity = new Vector2(
				((target.transform.position - this.transform.position).normalized.x
			 /Mathf.Abs((target.transform.position - this.transform.position).normalized.x)*speed),0);

			if((this.collider2D.bounds.min.x + rigidbody2D.velocity.x * Time.deltaTime < ground.collider2D.bounds.min.x && rigidbody2D.velocity.x < 0) ||
			   (this.collider2D.bounds.max.x + rigidbody2D.velocity.x * Time.deltaTime > ground.collider2D.bounds.max.x && rigidbody2D.velocity.x > 0) ||
			   wallhit)
				rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);

			wallhit = false;
		}
	}

	public override void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Platform") {
			ground = coll.gameObject;
			this.transform.up = trueup;
		} else if (coll.gameObject.tag == "Wall") {
			wallhit = true;
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
