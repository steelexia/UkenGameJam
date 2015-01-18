using UnityEngine;
using System.Collections;


/// <summary>
/// Mobile Unit
/// </summary>
public class Mob : Entity {

    public float hp;
    public float xp;
    public float atkDamage;
    public float speed = 10;
    Vector3 velocity = Vector3.zero;
	// Use this for initialization
    new void Start () {
        base.Start();
      
	}
	
	// Update is called once per frame
	public void Update () {
        velocity -= rigidbody.velocity;
        velocity.y = 0;
        if (velocity.sqrMagnitude > speed * speed) velocity = velocity.normalized * speed;
        rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        velocity = Vector3.zero;
	}

  
   	public void SetVelocity(Vector3 vel){
		velocity = vel;
	}
}
