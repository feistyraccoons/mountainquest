using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplaySelectedWeapon : MonoBehaviour
{

	private bool meleeSelected = true;

	// Use this for initialization
	void Start ()
	{
		GameObject.FindGameObjectWithTag ("MeleeImage").GetComponent<Image>().color = Color.red;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		string hp="";
		if (player != null)
			hp = player.GetComponent<Player> ().GetHP ().ToString ();
		Text textComponent = GameObject.FindGameObjectWithTag ("Status").GetComponent<Text> ();
		textComponent.text = "Health: " + hp;
	}

	public void UpdateMelee ()
	{
		if (meleeSelected)
			GameObject.FindGameObjectWithTag ("MeleeImage").GetComponent<ChangeWeapon> ().UpdateWeapon ();
		else {
			meleeSelected = true;
			GameObject.FindGameObjectWithTag ("MeleeImage").GetComponent<Image>().color = Color.red;
			GameObject.FindGameObjectWithTag ("RangedImage").GetComponent<Image>().color = Color.white;
		}
	}

	public void UpdateRanged ()
	{
		if (!meleeSelected)
			GameObject.FindGameObjectWithTag ("RangedImage").GetComponent<ChangeWeapon> ().UpdateWeapon ();
		else {
			meleeSelected = false;
			GameObject.FindGameObjectWithTag ("RangedImage").GetComponent<Image> ().color = Color.red;
			GameObject.FindGameObjectWithTag ("MeleeImage").GetComponent<Image> ().color = Color.white;
		}
	}
}
