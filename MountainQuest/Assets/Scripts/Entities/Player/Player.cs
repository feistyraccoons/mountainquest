using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	public bool isAiming = false;
	public GameObject ClickObj;
	public GameObject ClickObjBoost;
	public GameObject CreateRedirectSphere;
	public GameObject CreateBoostSphere;
//	private bool BoostMade = false;
	private bool RedirectMade = false;
	
	public Vector3 meh;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 mouse = Input.mousePosition;
		mouse.z = 10;
		Vector3 mPos = Camera.main.ScreenToWorldPoint(mouse);




//		if (Input.GetKeyDown(KeyCode.Q)) {
//			print ("HELLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
		if (Input.GetMouseButtonDown(2)	)
			{
				CreateBoostSphere = (GameObject)Instantiate (ClickObjBoost, mPos, Quaternion.identity);
//				BoostMade = true;
			}
			
//		}
		else if (Input.GetMouseButtonDown(1))
		{


				isAiming = true;
	


			
		}
		if (isAiming) {
				
				
				if(RedirectMade == false)
				{
					CreateRedirectSphere = (GameObject)Instantiate(ClickObj, mPos, Quaternion.identity);
					RedirectMade = true;
				}


				Vector3 aimDirection = mPos - CreateRedirectSphere.transform.position;
			aimDirection.Normalize();
			//print("HELOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
				//CreateRedirectSphere.transform.rotation = Quaternion.LookRotation(aimDirection);
				//CreateRedirectSphere.transform.rotation = Quaternion.LookRotation(aimDirection);
			meh = new Vector3(0,-1,0);

			float angle = Vector3.Angle(aimDirection, new Vector3(0,1,0));
			Vector3 cross = Vector3.Cross(aimDirection, new Vector3(0,1,0));
			if (cross.z > 0)
				angle = 360 - angle;
				CreateRedirectSphere.GetComponent<RedirectSphere>().ChangeDirection(angle,aimDirection );
				//Debug.Log(CreateRedirectSphere.Direction);

			//			float ang = Vector3.Angle(new Vector3(0,1,0), aimDirection);
			//			print (aimDirection);

			}
			if (Input.GetMouseButtonUp(1))
			{
				isAiming = false;
				CreateRedirectSphere = null;
				CreateBoostSphere = null;
//				BoostMade = false;
				RedirectMade = false;
				
			}
			
		}

	}


