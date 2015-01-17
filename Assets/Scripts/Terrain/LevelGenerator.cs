using UnityEngine;
using System.Collections;
using System;

public class LevelGenerator
{
    //public static int currentSeed = UnityEngine.Random.Range(0,100000);
    public static int currentSeed = 123;

    public static void generate(Level level, int width, int height)
    {
        level.tiles = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float perlinX, perlinY;
                perlinX = i * 0.3f;
                perlinY = j * 0.3f;

                if (perlinNoise(perlinX,perlinY,0.4f) > -0.2f)
                {
                    Tile tile = new Tile(Tile.Type.STONE);

                    tile.spawn(new Vector3(i * 2, 0, j * 2));

                    level.tiles [i, j] = tile;
                }
            }
        }
    }

    /**
     * Returns a pseudo random number between -1.0 to 1.0
     * Has 6 levels of octaves which generate different numbers
     */ 
    public static float noise (int x, int y, int octave)
    {
        int n = x + y * (57+currentSeed);
        n = (n << 13) ^ n;
        
        if (octave == 0)
            return (float)(1.0 - ((n * ((n * n * 19571) + 576377) + 1376312589) & 0x7fffffff) / 1073741824.0);
        else if (octave == 1)
            return (float)(1.0 - ((n * ((n * n * 22123) + 746939) + 1841957833) & 0x7fffffff) / 1073741824.0);
        else if (octave == 2)
            return (float)(1.0 - ((n * ((n * n * 23623) + 365147) + 1419576371) & 0x7fffffff) / 1073741824.0);
        else if (octave == 3)
            return (float)(1.0 - ((n * ((n * n * 25031) + 856343) + 1519576381) & 0x7fffffff) / 1073741824.0);
        else if (octave == 4)
            return (float)(1.0 - ((n * ((n * n * 20269) + 277427) + 1119515899) & 0x7fffffff) / 1073741824.0);
        else 
            return (float)(1.0 - ((n * ((n * n * 32609) + 793379) + 1000085861) & 0x7fffffff) / 1073741824.0);      
    }   
    
    /**
     * Cosine interpolation that smoothes the point x inbetween point a and b
     */ 
    public static float interpolate (float a, float b, float x)
    {
        float f = (float)(1 - Math.Cos (x * Math.PI)) * 0.5f;
        return (a * (1 - f)) + (b * f);
    }
    
    /**
     * Smoothes the noise in 2D
     */ 
    public static float smoothNoise2D (int a, int b, int octave)
    {
        
        float corners = (noise (a - 1, b - 1, octave) + noise (a + 1, b - 1, octave) + noise (a - 1, b + 1, octave) + noise (a + 1, b + 1, octave))/16;
        float sides = (noise (a - 1, b, octave) + noise (a + 1, b, octave) + noise (a, b - 1, octave) + noise (a, b + 1, octave))/8;
        float center = noise (a, b, octave)/4;
        return corners + sides + center;
        //return noise (a,b,octave);
    }
    
    public static float interpolatedNoise(float x, float y, int octave)
    {
        int intX = (int)x;
        //int intX = (int)Math.Round(x);
        float fractionalX;
        if (x > 0)
            fractionalX = x - intX;
        else
            fractionalX = 1-(x - intX);
        
        int intY = (int)y;
        //int intY = (int)Math.Round(y);
        float fractionalY;
        if (y > 0)
            fractionalY = y - intY;
        else
            fractionalY = 1-(y - intY);
        
        float v1 = smoothNoise2D (intX, intY, octave);
        float v2 = smoothNoise2D (intX + 1, intY, octave);
        float v3 = smoothNoise2D (intX, intY + 1, octave);
        float v4 = smoothNoise2D (intX + 1, intY + 1, octave);
        
        return interpolate (interpolate(v1, v2, fractionalX), interpolate(v3, v4, fractionalX), fractionalY);
    }
    
    /**
     * A 2D perlin noise function at (x,y), with a persistence variable, p.
     * p -> 1 results in very spiky noise, while p -> 0 results in flat noise
     */ 
    public static float perlinNoise(float x, float y, float p)
    {
        float total = 0;
        
        //6 octaves
        for (int i = 0; i < 6; i++)
        {
            float f = (float)Math.Pow(1.1f,i);
            float a = (float)Math.Pow(p,i);
            
            total += interpolatedNoise(x * f, y * f, i) * a;
        }
        return total;
        //return (total + 1f) / 2;
    }
}
