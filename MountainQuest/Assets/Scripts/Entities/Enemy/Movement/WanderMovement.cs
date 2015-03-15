using UnityEngine;
using System.Collections;

public class WanderMovement : Movement {

	// Use this for initialization
	public override void Start () {
		//DetermineDir ();
		speed = 2.0f;
		trueup = this.transform.up;
	}
	
	// Update is called once per frame
	public override void Update () {
		delayreac -= Time.deltaTime;

		if (delayreac <= 0.0f && ground == null)
			DetermineDir ();

		if (ground != null) {
			rigidbody2D.velocity = new Vector2(speed,0);
			if (!dir)
				rigidbody2D.velocity*=-1;
		} else {
			rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
			this.transform.up = preserveup;
		}
	}

	public override void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Platform") {
			ground = coll.gameObject;
			delayreac = 0.1f;
			this.transform.up = trueup;
		} else if (coll.gameObject.tag == "Wall") {
			transform.position -= (Vector3)rigidbody2D.velocity;
			dir = !dir;
		}
	}

	public void OnCollisionExit2D(Collision2D coll) {
		//DetermineDir ();
		if (ground == coll.gameObject) {
			ground = null;
			preserveup = this.transform.up;
			delayreac = 0.1f;
		}
	}

	void DetermineDir(){
		if (Random.Range (1, 6) > 3)
			dir = true; //right
		else
			dir = false; //left
	}
}
