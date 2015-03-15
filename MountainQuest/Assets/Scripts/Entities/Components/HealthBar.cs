using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{
	
	public float healthBarLength;
	private Health mHealth;

	// Use this for initialization
	void Start () {
		mHealth = GetComponent<Health> ();
		healthBarLength = Screen.width / 6;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI() {
		GUI.Box(new Rect(transform.position.x, transform.position.y+10, 10 + healthBarLength, 30), mHealth.m_fHealth + "/" + mHealth.m_fMaxHealth);
	}
	
	public void AddjustBar() {
		healthBarLength = (Screen.width / 6) * (mHealth.m_fHealth / mHealth.m_fMaxHealth);
	}
}