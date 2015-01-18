using UnityEngine;
using System.Collections;

public class Player : Mob {

	// Use this for initialization
	new void Start () {
        base.Start();
        hp = BaseStats.BASEPLAYERHP;
        atkDamage = BaseStats.BASEPLAYERATK;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            BreakBlock();
            animation.Play("human_attack");
        }
	
	}

    void BreakBlock()
    {
        RaycastHit hit;
         
      
        if(Physics.Raycast(this.transform.position - new Vector3(0,1,0), transform.forward,out hit,1))
        {
            Block block;
            if((block = hit.collider.gameObject.GetComponent<Block>())!=null)
            {
                block.Damage(atkDamage);
            }
        }
    }
}
