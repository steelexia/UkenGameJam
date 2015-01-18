using UnityEngine;
using System.Collections;

public class Player : Mob {

    /* number of hits left on pickaxe, 0 means no pickaxe */
    public int PickaxeDurability; 

    /* number of throwing spears left */
    public int SpearCount;

    /* number of meat left */
    public int MeatCount;

    public UIProgressBar progressBar;
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

        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            Heal(50f);
        }
        progressBar.value = hp / BaseStats.BASEPLAYERHP;
	}

    void BreakBlock()
    {
        RaycastHit hit;

        bool playedBreak = false;
      
        if(Physics.Raycast(this.transform.position + new Vector3(0,0,0), transform.forward,out hit,1.5f))
        {
            Block block;
            if((block = hit.collider.gameObject.GetComponent<Block>())!=null)
            {
                block.Damage(atkDamage);

                if (PickaxeDurability == 0)
                    animation.Play("human_break_0");
                else
                    animation.Play("human_break_1");

                playedBreak = true;
            }
        }

        if (!playedBreak)
            animation.Play("human_attack");
    }

    void OnTriggerStay(Collider other) {
        GameObject go = other.gameObject;

        if (go.tag.Equals("Item"))
        {
            Item item = go.GetComponent<Item>();

            if (item.TimeUntilPickup > 0)
                return;

            if (item.type == Item.Type.MEAT)
            {
                MeatCount++;
                Destroy(item.gameObject);
            }
            else if (item.type == Item.Type.SPEAR)
            {
                SpearCount++;
                Destroy(item.gameObject);
            }
            else if (item.type == Item.Type.PICKAXE && PickaxeDurability == 0)
            {
                PickaxeDurability = 40;
                Destroy(item.gameObject);
            }
        }
    }
}
