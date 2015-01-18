using UnityEngine;
using System.Collections;


/// <summary>
/// Mobile Unit
/// </summary>
public class Mob : Entity {

    public float hp;
    public float xp;
    public float atkDamage;
    public float attackCooldown;
    public float currentCooldown;
    public float speed = 10;
    public float attackDistance = 1.5f;
    Vector3 velocity = Vector3.zero;
    public AudioManager audioManager;
    
	// Use this for initialization
     protected virtual void Start () {
        base.Start();
        currentCooldown = attackCooldown;
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	public void Update () {
        currentCooldown += Time.deltaTime;
        currentCooldown = Mathf.Clamp(attackCooldown,0,attackCooldown);
	}

  
   	public void SetVelocity(Vector3 vel){
		velocity = vel;
	}
}
