using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {

    public static Dictionary<Type, GameObject> allBlockModels;

    public float health;
    public bool alive;
    public int i, j;
    public Level level;
    public enum Type{
        STONE,
        BORDER,
    }
	
    public Type type;

    public Block (Type type)
    {
        this.type = type;
        this.alive = false;
    }
   public void Init(Type type, Level level, int i, int j)
    {
        this.type = type;
        this.alive = true;
        this.level = level;
        this.i = i;
        this.j = j;
    }
    void Awake()
    {
        health = BaseStats.BASEBLOCKHP;
    }
    public void Damage(float damage)
    {
        if (type == Type.BORDER)
            return;

        health -= damage;

        //TODO: need to find all childs and do this, but also gotta change renderer back after a delay
        /*
           Color c =  gameObject.renderer.material.color;
          c.b  = Mathf.Max(0,health/BaseStats.BASEBLOCKHP);
          c.g = Mathf.Max(0, health / BaseStats.BASEBLOCKHP);
         gameObject.renderer.material.color= c;

        */
        
        if(health<=0)
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        level.destroyBlock(i, j);
        Destroy(this.gameObject);
    }
    public static void loadResources()
    {
        allBlockModels = new Dictionary<Type, GameObject>();

        GameObject model; //temp varaible

        model = (GameObject)Resources.Load("block_stone", typeof(GameObject));
        allBlockModels.Add(Type.STONE, model);

        model = (GameObject)Resources.Load("block_border", typeof(GameObject));
        allBlockModels.Add(Type.BORDER, model);
    }
}
