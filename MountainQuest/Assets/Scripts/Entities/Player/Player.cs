using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float attackRate = 0.25f;
	private float attackTimer = 0;
	public Projectile arrow;
	public bool isAiming = false;
	public GameObject ClickObj;
	public GameObject ClickObjBoost;
	private GameObject CreateRedirectSphere;
	private GameObject CreateBoostSphere;
	private bool RedirectMade = false, melee = false, facingRight = true;
	public float SphereDistance = 21;
	public float PlayerAliveTimer = 8.0f;
	private int RSphereTotal = 0;
	public int RSphereCap = 3;
	private int BSphereTotal = 0;
	public int BSphereCap = 3;

	void Start ()
	{
	}
	
	public override void Update ()
	{
		Vector3 mouse = Input.mousePosition;
		mouse.z = 10;
		Vector3 mPos = Camera.main.ScreenToWorldPoint (mouse);


		if (rigidbody2D.velocity.x > 0)
			facingRight = true;
		else if (rigidbody2D.velocity.x < 0)
			facingRight = false;

		if (Input.GetMouseButtonDown (2) && BSphereTotal != BSphereCap) {
			
			bool goCreate = true;
			foreach (BoostSphere ball in GameObject.FindObjectsOfType<BoostSphere>()) {
				if (Vector3.Distance (mPos, ball.transform.position) < SphereDistance && goCreate) 
					goCreate = false;
			}
			
			if (goCreate) {
				CreateBoostSphere = (GameObject)Instantiate (ClickObjBoost, mPos, Quaternion.identity);
				BSphereTotal += 1;
				CreateBoostSphere.GetComponent<BoostSphere> ().SetOwner (this);
			}
			
		} else if (Input.GetMouseButtonDown (1) && RSphereTotal != RSphereCap) 
			isAiming = true;

		if (Input.GetKey (KeyCode.Alpha1)) 
			melee = true;
		else if (Input.GetKey (KeyCode.Alpha2)) 
			melee = false;

		attackTimer -= Time.deltaTime;
		if (Input.GetMouseButton (0) && attackTimer <= 0) {
			if (!melee) {
				Projectile firedArrow = (Projectile)Instantiate (arrow, transform.position + new Vector3 (facingRight ? 1 : -1, 0, 0), transform.rotation);
				firedArrow.Shoot (new Vector2 (facingRight ? 1 : -1, 0));
			}
			attackTimer = attackRate;
		}

		
		if (isAiming) {
			if (RedirectMade == false) {
				
				bool goCreate = true;
				foreach (RedirectSphere ball in GameObject.FindObjectsOfType<RedirectSphere>()) {
					if (Vector3.Distance (mPos, ball.transform.position) < SphereDistance && goCreate)
						goCreate = false;
				}
				if (goCreate) {
					CreateRedirectSphere = (GameObject)Instantiate (ClickObj, mPos, Quaternion.identity);
					RSphereTotal += 1;
					CreateRedirectSphere.GetComponent<RedirectSphere> ().SetOwner (this);
					RedirectMade = true;
				}
			}
			if (CreateRedirectSphere != null) {
				Vector3 aimDirection = mPos - CreateRedirectSphere.transform.position;
				aimDirection.Normalize ();
				float angle = Vector3.Angle (aimDirection, new Vector3 (0, 1, 0));
				Vector3 cross = Vector3.Cross (aimDirection, new Vector3 (0, 1, 0));
				if (cross.z > 0)
					angle = 360 - angle;
				
				CreateRedirectSphere.GetComponent<RedirectSphere> ().ChangeDirection (angle, aimDirection, PlayerAliveTimer);
			}


		}

		if (Input.GetMouseButtonUp (1)) {
			isAiming = false;
			CreateRedirectSphere = null;
			CreateBoostSphere = null;
			RedirectMade = false;
		}
				
		base.Update ();
	
	}
	

	public void RemoveRSphere ()
	{
		RSphereTotal -= 1;
	
	}

	public void RemoveBSphere ()
	{
		BSphereTotal -= 1;
	}

}


