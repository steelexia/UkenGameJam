﻿using UnityEngine;
using System.Collections;


/// <summary>
/// Mobile Unit
/// </summary>
public class Mob : Entity {

    public float hp;
    public float xp;
    public float atkDamage;
    public float attackRate = 0.8f;
    public float attackCooldown;
    public float currentCooldown;
    public float speed = 10;
    public float attackDistance = 1.5f;
    public Animation anim;
    Vector3 velocity = Vector3.zero;
    public AudioManager audioManager;
    
	// Use this for initialization
     protected virtual void Start () {
        base.Start();
        currentCooldown = attackCooldown;
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
     protected virtual void Update()
     {
        currentCooldown += Time.deltaTime;
        currentCooldown = Mathf.Clamp(attackCooldown,0,attackCooldown);
	}
  
   	public void SetVelocity(Vector3 vel){
		velocity = vel;
	}

    public void Attack(string layer)
    {
        RaycastHit hit;
        anim.Play("human_attack");
        audioManager.PlayAudio(0);

        if (Physics.Raycast(this.transform.position + new Vector3(0, 0, 0), transform.forward, out hit, 1.5f, ~LayerMask.NameToLayer(layer)))
        {
            Mob player;
            if ((player = hit.collider.gameObject.GetComponent<Mob>()) != null)
            {
                audioManager.PlayAudio(1);
                player.Damage(atkDamage);
            }
        }
    }
    public void Damage(float damage)
    {
        if (this == null)
            return;
         hp -= damage;


       

        if (hp<= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}
