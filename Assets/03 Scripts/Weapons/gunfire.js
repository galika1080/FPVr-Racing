var n = 0;
var gunshot1 : AudioSource;
var gunshot2 : AudioSource;
var firing = false;
var rpm = 500;
var shotDelay : float;
var ammo = 30;
var flash : ParticleSystem;
var bullet : ParticleSystem;
var anim : Animator;
var reloading = false;

var GunType = "SemiAuto";

function Update () {

if(Input.GetKeyDown('r')) {
	StartCoroutine("Reload");
	}

if(Input.GetKey("w"))
	anim.SetBool("walking", true);
else
	anim.SetBool("walking", false);

if(Input.GetKey("w") && Input.GetKey("left shift"))
	anim.SetBool("running", true);
else
	anim.SetBool("running", false);

if(ammo > 0 && reloading == false) {
	if(Input.GetMouseButtonDown(0)) {
		if(GunType == "FullAuto") {
			if(firing == false) {
				anim.SetBool("Firing", true);
				firing = true;
				StartCoroutine(GunType);
				}
			}
		}
	if(GunType == "SemiAuto")
		StartCoroutine(GunType);
	}
}

function Start () {
shotDelay = (60.0/rpm);
}


function FullAuto () {
if(ammo/2.0 == ammo/2)
	gunshot1.Play();
else
	gunshot2.Play();
ammo--;
print("Bang!" + ammo);
flash.Emit(1);
bullet.Emit(1);
yield WaitForSeconds (shotDelay);
if(Input.GetMouseButton(0)) {
	if(ammo > 0)
		StartCoroutine("FullAuto");
	else {
		anim.SetBool("Firing", false);
		firing = false;
		}
	}
else {
	firing = false;
	anim.SetBool("Firing", false);
	}
}

function SemiAuto () {
print("Bang!" + ammo);
gunshot1.Play();
ammo--;
flash.Emit(1);
bullet.Emit(1);
yield WaitForSeconds (shotDelay);
}

function Reload () {
	reloading = true;
	anim.SetBool("Reloading", true);
	yield WaitForSeconds(1);
	reloading = false;
	anim.SetBool("Reloading", false);
	ammo = 35;
}