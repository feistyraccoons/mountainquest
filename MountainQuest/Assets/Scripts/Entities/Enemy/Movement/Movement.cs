using UnityEngine;
using System.Collections;

[System.Serializable]
public class Movement: MonoBehaviour {

	public bool dir;
	public float speed = 2.5f;
	public Vector3 preserveup;
//	public Vector3 Movevec;
	public Vector3 trueup;
	public float delayreac = 0.1f;
	protected GameObject ground = null;
	// Use this for initialization
	virtual public void Start () {
	
	}
	
	// Update is called once per frame
	virtual public void Update () {
		
	}


	virtual public void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Wall") {
			rigidbody2D.velocity *= -1;
		}
	}
}
