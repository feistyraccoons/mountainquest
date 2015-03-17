using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	public float maxSpeed = 1.0f;
	public float jumpSpeed = 1;
	private bool onGround = false;
	private Vector2 preserveUp;

	// Use this for initialization
	void Start () {
		preserveUp = transform.up;
	}
	
	// Update is called once per frame
	void Update () {


		rigidbody2D.velocity = new Vector2 (Input.GetAxis ("Horizontal")* 10,rigidbody2D.velocity .y);


		if (onGround && Input.GetAxis ("Vertical") > 0) {
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpSpeed);
		}


		if (Mathf.Abs( rigidbody2D.velocity.x) > maxSpeed)
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.normalized.x * maxSpeed,rigidbody2D.velocity.y);

		transform.up = preserveUp;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform" && coll.gameObject.transform.position.y < transform.position.y)
			onGround = true;
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform")
			onGround = false;
	}
}