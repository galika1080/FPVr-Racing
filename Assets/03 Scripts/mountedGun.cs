using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent (typeof (interactiveObject_pivoting))]

public class mountedGun : MonoBehaviour {

	interactiveObject_pivoting ioScript;
	SteamVR_Controller.Device device;
    SteamVR_TrackedObject controller;
	
	int n = 0;
	public AudioSource gunshot1;
	public AudioSource gunshot2;
	bool firing = false;
	public int rpm = 500;
	float shotDelay;
	public int ammo = 30;
	public ParticleSystem flash;
	public Rigidbody projectile;
	public int speed = 400;
	//public bool targeted = false;
	//public Transform target;
	//var anim : Animator;
	bool reloading = false;

	public string GunType = "SemiAuto";
	
	void Start () {
		ioScript = gameObject.GetComponent<interactiveObject_pivoting>();
	}
	
	void Update () {
		if(ioScript.getHost() != null) {
			controller = ioScript.getHost().gameObject.GetComponent<SteamVR_TrackedObject>();
			device = SteamVR_Controller.Input((int)controller.index);
			
			//trigger = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			
			if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
				
			}
			
			if(ammo > 0 && reloading == false) {
				if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
					if(GunType == "FullAuto") {
						if(firing == false) {
							//anim.SetBool("Firing", true);
							firing = true;
							StartCoroutine(FullAuto());
						}
					}
					if(GunType == "SemiAuto")
						StartCoroutine(SemiAuto());
				}
			}
			
			
		}
	}
	
	private IEnumerator SemiAuto () {
		gunshot1.Play();
		ammo--;
		flash.Emit(1);

		Rigidbody clone;
		//clone = Instantiate(projectile, transform.position + 5*transform.forward, transform.rotation);
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
		if(ammo%2 == 0)
			gunshot1.Play();
		else
			gunshot2.Play();
		ammo--;
		flash.Emit(1);

		Rigidbody clone;
		//clone = Instantiate(projectile, transform.position + 5*transform.forward, transform.rotation);
		clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		clone.velocity = -transform.forward * speed;
		
		yield return new WaitForSeconds (shotDelay);
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			if(ammo > 0)
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
