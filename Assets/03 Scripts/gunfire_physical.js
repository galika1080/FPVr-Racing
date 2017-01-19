var n = 0;
var gunshot1 : AudioSource;
var gunshot2 : AudioSource;
var firing = false;
var rpm = 500;
var shotDelay : float;
var ammo = 30;
var flash : ParticleSystem;
var projectile : Rigidbody;
var speed = 400;
var targeted = false;
var target : Transform;
//var anim : Animator;
var reloading = false;

var GunType = "SemiAuto";

function Update () {

if(Input.GetKeyDown('r')) {
	StartCoroutine("Reload");
	}

if(ammo > 0 && reloading == false) {
	if(Input.GetKeyDown('space')) {
		if(GunType == "FullAuto") {
			if(firing == false) {
				//anim.SetBool("Firing", true);
				firing = true;
				StartCoroutine(GunType);
				}
			}
		if(GunType == "SemiAuto")
			StartCoroutine(GunType);
		}
	}
}

function Start () {
shotDelay = (60.0/rpm);
}


function FullAuto () {
if(ammo%2 == 0)
	gunshot1.Play();
else
	gunshot2.Play();
ammo--;
print("Bang!" + ammo);
flash.Emit(1);

var clone : Rigidbody;
clone = Instantiate(projectile, transform.position + 5*transform.forward, transform.rotation);
clone.velocity = transform.forward * speed;
if(targeted) {
	if(clone.GetComponent(clusterbomb) != null)
		clone.GetComponent(clusterbomb).target = target;
	if(clone.GetComponent(explodeInRange) != null)
		clone.GetComponent(explodeInRange).target = target;
	if(clone.GetComponent(lookAt) != null)
		clone.GetComponent(lookAt).target = target;
	}
yield WaitForSeconds (shotDelay);
if(Input.GetKey('space')) {
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

function SemiAuto () {
print("Bang!" + ammo);
gunshot1.Play();
ammo--;
flash.Emit(1);

var clone : Rigidbody;
clone = Instantiate(projectile, transform.position + 5*transform.forward, transform.rotation);
clone.velocity = transform.forward * speed;

if(targeted) {
	if(clone.GetComponent(clusterbomb) != null)
		clone.GetComponent(clusterbomb).target = target;
	if(clone.GetComponent(explodeInRange) != null)
		clone.GetComponent(explodeInRange).target = target;
	if(clone.GetComponent(lookAt) != null)
		clone.GetComponent(lookAt).target = target;
	}

yield WaitForSeconds (shotDelay);
}

function Reload () {
	reloading = true;
	//anim.SetBool("Reloading", true);
	yield WaitForSeconds(1);
	reloading = false;
	//anim.SetBool("Reloading", false);
	ammo = 35;
}