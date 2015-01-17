using UnityEngine;
using System.Collections;

public class Block  {

    public enum Type{
        STONE,
    }
	
    public Type type;

    public Block (Type type)
    {
        this.type = type;
    }
}
