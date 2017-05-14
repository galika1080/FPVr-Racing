using UnityEngine;
using System.Collections;

public class magazine : MonoBehaviour {
	
	private int ammo;
	
	public int startingAmmo;
	
	void Start () {
		ammo = startingAmmo;
	}
	
	public int getAmmo () {
		return ammo;
	}
}