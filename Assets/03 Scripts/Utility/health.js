var health = 100;

function Update () {
print(health);
if(health < 0)
	Die();
}

function OnCollisionEnter (collision : Collision) {
if(collision.gameObject.tag == "9mm")
	health = health - 5;
if(collision.gameObject.tag == "shrapnel")
	health = health - 15;
}

function Die () {
Destroy(gameObject);
}