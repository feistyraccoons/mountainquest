using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChangeWeapon : MonoBehaviour {

	public List<Sprite> weapons;
	private int ListIndex = 0;

	// Use this for initialization
	void Start () {
		weapons = new List<Sprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateWeapon(){
		++ListIndex;
		if (ListIndex >= weapons.Count)
			ListIndex = 0;

//		GetComponent<Image> ().sprite = weapons [ListIndex];
	}
}