using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int damage;
    public int thrower; //0 = player, 1 = NPC

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate((gameObject.transform.forward * 5f - gameObject.transform.up * 0.2f) * Time.deltaTime);
	}
}
