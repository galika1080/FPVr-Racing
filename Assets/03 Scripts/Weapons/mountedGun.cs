using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof (interactiveObject_pivoting))]

public class mountedGun : MonoBehaviour {

	interactiveObject_pivoting ioScript;
	SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;		// SEMI-AUTO IS PROBABLY BROKEN. PLEASE TEST
	
	int n = 0;
	
	public GameObject magObject;
	private magazineAlias mag;
	public AudioSource gunshot1;
	public AudioSource gunshot2;
	bool firing = false;
	public int rpm = 500;
	float shotDelay;
	public ParticleSystem flash;
	public Rigidbody projectile;
	public int speed = 400;
	
	//public bool targeted = false;		IMPLEMENT LOCK-ON CAPABILITY
	//Transform target;					USING RAYCAST TO FIND TARGET
	
	//var anim : Animator;				IMPLEMENT RECOIL ANIMATION
	
	bool reloading = false;

	public string GunType = "SemiAuto";
	
	void Start () {
		ioScript = gameObject.GetComponent<interactiveObject_pivoting>();
		mag = magObject.GetComponent<magazineAlias>();
	}
	
	void Update () {
		if(ioScript.getHost() != null) {
			controller = ioScript.getHost().gameObject.GetComponent<SteamVR_TrackedObject>();
			device = SteamVR_Controller.Input((int)controller.index);
			
			//trigger = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
				// possibly implement triger tolerances
				
			if(!reloading && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
				if(GunType == "FullAuto") {
					if(firing == false && mag.removeBullet()) {
						//anim.SetBool("Firing", true);
						firing = true;
						StartCoroutine(FullAuto());
					}
				}
				if(GunType == "SemiAuto" && mag.removeBullet())
					StartCoroutine(SemiAuto());
			}
		}
	}
	
	private IEnumerator SemiAuto () {
		gunshot1.Play();
		flash.Emit(1);

		Rigidbody clone;
		clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		clone.velocity = -transform.forward * speed;

		/*if(targeted) {
			if(clone.GetComponent<clusterbomb>() != null)
				clone.GetComponent<clusterbomb>().target = target;
			if(clone.GetComponent<explodeInRange>() != null)
				clone.GetComponent<explodeInRange>().target = target;
			if(clone.GetComponent<lookAt>() != null)
				clone.GetComponent<lookAt>().target = target;
		}*/

		yield return new WaitForSeconds (shotDelay);
	}
	
	private IEnumerator FullAuto () {
		//if(ammo%2 == 0)
		//	gunshot1.Play();		FIX ME PLEASE
		//else
		gunshot2.Play();
	
		flash.Emit(1);

		Rigidbody clone;
		clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		clone.velocity = -transform.forward * speed;
		
		yield return new WaitForSeconds (shotDelay);
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			if(mag.removeBullet())
				StartCoroutine(FullAuto());
			else {
				//anim.SetBool("Firing", false);
				firing = false;
			}
		}
		else {
			firing = false;
			//anim.SetBool("Firing", false);
		}
	}
}
