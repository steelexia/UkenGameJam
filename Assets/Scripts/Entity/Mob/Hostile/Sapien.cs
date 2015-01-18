using UnityEngine;
using System.Collections;

public class Sapien : NPC {

	// Use this for initialization

    new void Start () {
        base.Start();
	    //TODO
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	    //TODO
	}

    public override void Die()
    {
        if (UnityEngine.Random.Range(0,1f) < 0.5f)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0, UnityEngine.Random.Range(-0.5f, 0.5f));
                GameObject itemGO = (GameObject)GameObject.Instantiate(Item.allItemModels [Item.Type.SPEAR], gameObject.transform.position + randomOffset, Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0,360), 0)));
                Item item = itemGO.GetComponent<Item>();
                item.Init(Item.Type.SPEAR);
                item.Drop();
            }
        }
    }
}
