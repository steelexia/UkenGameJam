using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

    public Player player;
    public Level level;

	// Use this for initialization
	void Start () {
        Tile.loadResources();
        Block.loadResources();
        level = new Level(player);
	}  
	
	// Update is called once per frame
	void Update () {
        level.update();
	}
}
