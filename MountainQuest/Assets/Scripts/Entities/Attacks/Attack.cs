using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Attack : MonoBehaviour {
	
	public float attackTimer = 0;
	protected float attackDecrease = 0;
	public List<Weapon> theWeapons;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public virtual void Update () {
	}
}