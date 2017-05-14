@script RequireComponent(lookAt)

var projectile : Rigidbody;
var firingForce = 0;
var leadTarget = false;

var audiosrc : AudioSource;

var target : Transform;

@Tooltip ("Check if using guided projectiles")
var targeted = false;

@Tooltip ("single, burst, or auto")
var firemode = "single";

@Tooltip ("Time in seconds before sentry begins firing")
var startDelay = 0;

@Tooltip ("Time in seconds between bursts or single shots")
var spacing = 3.0;

@Tooltip ("How many shots fired per burst")
var burstCount = 5;

@Tooltip ("Time in seconds between each shot in a burst")
var burstSpacing = 0.15;

InvokeRepeating(firemode, startDelay, spacing);

var i = 0;

function Update () {
if(leadTarget) {
	var dist = Vector3.Distance(target.position, transform.position);
	var time = (dist / (firingForce / projectile.mass));
	GetComponent(lookAt).leadTime = time;
	}
}

function burstBuffer () {
yield WaitForSeconds(burstSpacing);
burst();
}

function burst () {
single();
i++;
if(i < burstCount)
	burstBuffer();
else
	i = 0;
}

function single () {
	var clone : Rigidbody;
	clone = Instantiate(projectile, transform.position, transform.rotation);
	clone.velocity = transform.TransformDirection (Vector3.forward * firingForce);
	audiosrc.Play();
	if(targeted) {
		if(clone.GetComponent(clusterbomb) != null)
			clone.GetComponent(clusterbomb).target = target;
		if(clone.GetComponent(explodeInRange) != null)
			clone.GetComponent(explodeInRange).target = target;
		if(clone.GetComponent(lookAt) != null)
			clone.GetComponent(lookAt).target = target;
		}
}

