using UnityEngine;
using System.Collections;

public class BoostSphere : MonoBehaviour
{

	public float DamageModifier = 1.5f;
	public float VelocityModifier = 5;
	public float AliveTimer = 7;
	public bool isAiming = false;
	public GameObject ClickObj;
	public GameObject Sphere;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
		AliveTimer -= Time.deltaTime;
		if (AliveTimer <= 0) {
			Destroy (this.gameObject); 		
			print (this.name);
			
		}
		
		if (AliveTimer <= 2) {
			SpriteRenderer mySR = GetComponent<SpriteRenderer> ();
			//Animator myA = GetComponent<Animator>();
			
			mySR.color = new Color (1, 1, 1, AliveTimer / 2);
		}

//		Vector3 mouse = Input.mousePosition;
//		mouse.z = 10;
//		Vector3 mPos = Camera.main.ScreenToWorldPoint(mouse);
//
//		if (Input.GetMouseButtonDown(0))
//		    {
//			//print("mouse");
//			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//
////			GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
////			go.transform.position = mPos;
//
//			if(ClickObj != null)
//			{
//				isAiming = true;
//				Sphere = (GameObject)Instantiate(ClickObj, mPos, Quaternion.identity);
//			}
//
//		}
//		if (isAiming && Sphere !=  null) {
//			Vector3 aimDirection = mPos - Sphere.transform.position;
//			aimDirection.Normalize();
//			Sphere.transform.rotation = Quaternion.LookRotation(aimDirection);
////			float ang = Vector3.Angle(new Vector3(0,1,0), aimDirection);
////			print (aimDirection);
//			if (Input.GetMouseButtonUp(0))
//			    {
//				isAiming = false;
//				Sphere = null;
//
//			}
//
//		}


	}
	void OnTriggerEnter2D (Collider2D other)
	{
		Projectile proj = other.GetComponent<Projectile> ();
		other.rigidbody2D.velocity *= VelocityModifier;
		
		if (proj != null) {
			proj.m_fDamage *= DamageModifier;
		}
//		other.rigidbody2D.velocity = Vector3.up * 5;
	}

//	void OnTriggerStay2D(Collider2D other)
//	{
//		float dist = Vector3.Distance (this.transform.position, other.transform.position);
//		if (dist < 0.1f) 
//		{
//
//			//other.rigidbody2D.velocity = Vector3.down * 2;
//			other.rigidbody2D.velocity *= 6;
//			
//		}
//	}
}
