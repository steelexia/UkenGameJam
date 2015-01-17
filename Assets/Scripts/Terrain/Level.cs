using UnityEngine;
using System.Collections.Generic;

public class Level{

    public int width, height;
    public Tile[,] tiles;
    public Block[,] block;
    public List<Mob> allMobs;

    public Level(Player player)
    {
        width = 64;
        height = 64;
        LevelGenerator.generate(this, width, height);

        allMobs = new List<Mob>();
        allMobs.Add(player);
    }

    public void update()
    {

    }
}
