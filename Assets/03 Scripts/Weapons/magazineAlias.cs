using UnityEngine;
using System.Collections;

[RequireComponent (typeof (aliasObject))]

public class magazineAlias : MonoBehaviour {
	
	public int startingAmmo;
	private int ammo;
	
	private aliasObject alias;
	
	void Start () {
		ammo = startingAmmo;
		alias = gameObject.GetComponent<aliasObject>();
	}
	
	public void setAmmo(int a) {
		ammo = a;
	}
	
	public bool removeBullet () {
		if(!alias.inPlace)
			ammo = 0;
		
		if(ammo > 0) {
			ammo--;
			return true;
		} else return false;
	}
}