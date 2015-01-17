using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {

    public static Dictionary<Type, GameObject> allBlockModels;

    public float health;
    public enum Type{
        STONE,
    }
	
    public Type type;

    public Block (Type type)
    {
        this.type = type;
    }

    void Awake()
    {
        health = BaseStats.BASEBLOCKHP;
    }
    public void Damage(float damage)
    {
        health -= damage;

           Color c =  gameObject.renderer.material.color;
          c.b  = Mathf.Max(0,health/BaseStats.BASEBLOCKHP);
          c.g = Mathf.Max(0, health / BaseStats.BASEBLOCKHP);
         gameObject.renderer.material.color= c;

        
        if(health<=0)
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public static void loadResources()
    {
        allBlockModels = new Dictionary<Type, GameObject>();

        GameObject model; //temp varaible

        model = (GameObject)Resources.Load("block_stone", typeof(GameObject));
        allBlockModels.Add(Type.STONE, model);
    }
}
