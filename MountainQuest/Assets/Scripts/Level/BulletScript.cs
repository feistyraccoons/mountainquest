using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float Lifetime = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Lifetime -= Time.deltaTime;
		if (Lifetime <= 0.0f)
			Destroy (this.gameObject);
	}
}
