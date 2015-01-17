using UnityEngine;
using System.Collections.Generic;
using System;

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

    protected void removeSingleEdge(int i, int j, bool west, bool south)
    {
        int addX = west ? -1 : 1;
        int addZ = south ? -1 : 1;
        String eastWest = west ? "west" : "east";
        String northSouth = south ? "south" : "north";

        if (i + addX < 0 || i + addX >= width)
        	addX = 0;
        if (j + addZ < 0 || j + addZ >= height)
        	addZ = 0;

        bool leftRight = block[i + addX, j] == null;
        bool upDown = block [i, j + addZ] == null;
        bool diag = block [i + addX, j + addZ] == null;

        if (leftRight && upDown && diag)
        {
            GameObject go = block[i,j].gameObject.transform.FindChild("block_stone_" + northSouth + "_" + eastWest).gameObject;
            go.SetActive(false);
        }
    }

    public void removeEdges(int i, int j)
    {
        if(i >=0 && i < width && j >=0 && j < height 
           && block[i,j] != null)
        {
            for (int k = 0; k < 4; k++) {
                bool west = k == 0 || k == 3;
                bool south = k > 1;
                removeSingleEdge(i, j, west, south);
            }
        }
    }

    public void destroyBlock(int i, int j)
    {
        block[i, j] = null;

        for (int k = 0; k < 9; k++)
        {
            int neighbourI = (i-1) + k % 3;
            int neighbourJ = (j-1) + k / 3;
            removeEdges(neighbourI, neighbourJ);
        }
    }
}
