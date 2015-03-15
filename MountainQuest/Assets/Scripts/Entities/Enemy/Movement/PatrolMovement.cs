using UnityEngine;
using System.Collections;

public class PatrolMovement : Movement {

	// Use this for initialization
	public override void Start () {
		speed = 2.0f;
		trueup = this.transform.up;
	}
	
	// Update is called once per frame
	public override void Update () {
		if (ground != null) {

			delayreac -= Time.deltaTime;

			rigidbody2D.velocity = new Vector2(speed,0);

			if(this.collider2D.bounds.min.x + rigidbody2D.velocity.x * Time.deltaTime < ground.collider2D.bounds.min.x)
				dir = true;
			else if (this.collider2D.bounds.max.x + rigidbody2D.velocity.x * Time.deltaTime > ground.collider2D.bounds.max.x)
				dir = false;

			if (!dir)
				rigidbody2D.velocity*=-1;
		}
	}

	public override void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Platform") {
			ground = coll.gameObject;
			this.transform.up = trueup;
		} else if (coll.gameObject.tag == "Wall") {
			transform.position -= (Vector3)rigidbody2D.velocity;
			dir = !dir;
		}
	}

	public void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform") {
			ground = null;
		}
	}

}
