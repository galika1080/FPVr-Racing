using UnityEngine;
using System.Collections;

public class ControllerTestInput : MonoBehaviour {
	
	SteamVR_TrackedObject trackedObj;
	public Rigidbody projectile;
	public Transform barrel;
	bool firing = false;
	int ammo = 100;
	public float shotDelay = 0.5f;
	
	void Start() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update () {
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
			if(firing == false) {
				//anim.SetBool("Firing", true);
				firing = true;
				StartCoroutine("FullAuto");
			}
	}
	
	void shootThing () {
		Rigidbody proj;
        proj = Instantiate(projectile, barrel.position, barrel.rotation) as Rigidbody;
		proj.gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        proj.AddForce(barrel.forward * 5000);
	}
	
	IEnumerator FullAuto () {
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		/*if(ammo/2.0 == ammo/2)
			//gunshot1.Play();
		else*/
			//gunshot2.Play();
		ammo--;
		//Debug.Log("Bang!" + ammo);
		shootThing();
		//flash.Emit(1);
		//bullet.Emit(1);
		yield return new WaitForSeconds(shotDelay);
		if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
			if(ammo > 0)
				StartCoroutine("FullAuto");
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