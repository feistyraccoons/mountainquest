using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{
	
	public float healthBarLength = 10;
	private Health mHealth;

	void Start () {
		mHealth = GetComponent<Health> ();
		healthBarLength = Screen.width / 6;
	}
	
	void Update () {

	}
	
	void OnGUI() {
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		
		GUI.Box(new Rect(screenPosition.x-10,screenPosition.y-40,healthBarLength+30,20),mHealth.m_fHealth+"/"+mHealth.m_fMaxHealth);
	}
	
	public void AdjustBar() {
//		healthBarLength = (Screen.width / 6) * (mHealth.m_fHealth / mHealth.m_fMaxHealth);
	}
}