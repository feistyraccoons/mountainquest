using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Entity))]
public class Health : MonoBehaviour
{
	public float m_fHealth=100,m_fMaxHealth = 100.0f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_fHealth > m_fMaxHealth)
			m_fHealth = m_fMaxHealth;
	}

	public void heal (float _heal)
	{
		SendMessage ("AdjustBar");
		if (_heal <= 0) {
			m_fHealth += _heal;
			if (m_fHealth > m_fMaxHealth)
				m_fHealth = m_fMaxHealth;
		} else
			Debug.LogWarning ("Attempting to heal a negative value.");
	}

	public void takeDamage (float _damage)
	{
		SendMessage ("AdjustBar");
		if (_damage >= 0) {
				m_fHealth -= _damage;
				if(m_fHealth<0)
					m_fHealth=0;

			if (m_fHealth == 0) 
				SendMessageUpwards ("die");

		} else
			Debug.LogWarning ("Attempting to deal negative damage.");
	}
}
