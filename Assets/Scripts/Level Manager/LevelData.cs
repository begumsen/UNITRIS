using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    /*public int level;
    public int width;
    public int height;
    public string[,] colors;
    public int[] blocks;
    public string name;*/
    public int level;
    public int width;
    public int height;
    public string[,] colors; // Use List<List<string>> instead of string[,]
    public int[] blocks;
    public string name;

}
