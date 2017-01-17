#pragma strict

var projectileCount = 4;
var delay = 1.0;
var firingForce = 0;
var projectile : Rigidbody;
var target : Transform;

function Start () {
yield WaitForSeconds(delay);
var i = 0;
while(i < projectileCount) {
	var clone : Rigidbody;
	clone = Instantiate(projectile, transform.position, transform.rotation);
	clone.velocity = transform.TransformDirection (Vector3.forward * firingForce);
	clone.GetComponent(lookAt).target = target;
	clone.GetComponent(explodeInRange).target = target;
	i++;
	}
//Destroy(gameObject);
}