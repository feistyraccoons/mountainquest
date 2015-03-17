using UnityEngine;
using System.Collections;

public class Player : Entity
{
	public float attackRate = 0.25f;
	private float attackTimer = 0;
	public Projectile arrow;
	public bool isAiming = false;
	public GameObject instructionsUI;
	public GameObject ClickObj;
	public GameObject ClickObjBoost;
	private GameObject CreateRedirectSphere;
	private GameObject CreateBoostSphere;
	private bool RedirectMade = false, melee = false,instructions=false;
	public float SphereDistance = 21;
	public float PlayerAliveTimer = 8.0f;
	private int RSphereTotal = 0;
	public int RSphereCap = 3;
	private int BSphereTotal = 0;
	public int BSphereCap = 3;
	

	public override void Update ()
	{
		if(instructionsUI!=null)
		instructionsUI.SetActive (instructions);

		if(Input.GetKeyDown(KeyCode.I))
		   instructions=!instructions;

		Vector3 mouse = Input.mousePosition;
		mouse.z = 10;
		Vector3 mPos = Camera.main.ScreenToWorldPoint (mouse);


		if (!facingRight)
			transform.Rotate (new Vector3(0,-180,0));
		else
			transform.Rotate (new Vector3(0,0,0));

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

		if (Input.GetKey (KeyCode.Alpha1)) {
			GameObject.FindGameObjectWithTag ("HUD").GetComponent<DisplaySelectedWeapon> ().UpdateMelee ();
			melee = true;
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			GameObject.FindGameObjectWithTag ("HUD").GetComponent<DisplaySelectedWeapon> ().UpdateRanged ();
			melee = false;
		}
		attackTimer -= Time.deltaTime;
		if (Input.GetMouseButton (0) && attackTimer <= 0) {
			if (!melee) {
				Vector3 ArrowDirection = mPos - transform.position;
				float Arrowangle = Vector3.Angle (ArrowDirection, new Vector3 (0, 1, 0));
				Vector3 Arrowcross = Vector3.Cross (ArrowDirection, new Vector3 (0, 1, 0));
				if (Arrowcross.z > 0)
					Arrowangle = 360 - Arrowangle;
				ArrowDirection.Normalize ();
				Projectile firedArrow = (Projectile)Instantiate (arrow, transform.position + (ArrowDirection * 2), Quaternion.Euler (new Vector3 (0, 0,Arrowangle )));
				firedArrow.rigidbody2D.rotation = Arrowangle + 90;
				
				//firedArrow.Shoot (new Vector2 (facingRight ? 1 : -1, 0));
				firedArrow.Shoot (ArrowDirection);
				
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
	
public float GetHP ()
{
	return health.m_fHealth;
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


