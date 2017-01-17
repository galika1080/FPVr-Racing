var health = 1000;
var damage = 14;

function Update () {
print(health);
if(health < 0)
	Die();
}

function OnCollisionEnter (collision : Collision) {
if(collision.gameObject.tag == "9mm")
	health = health - damage;
}

function Die () {
Destroy(gameObject);
}