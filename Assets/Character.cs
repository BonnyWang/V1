using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int background;

    //property
    public int air;
    public int water;
    public int fire;
    public int ground;
    public int plant;
    public int soul;

    //sprite
    public Sprite sprite;
    
    //item
    public int[] itemlist;

    public Character(string name, int background, int air,int water,int fire,int ground,int plant,int soul,Sprite sprite)
    {
        this.air = air;
        this.water = water;
        this.fire = fire;
        this.ground = ground;
        this.plant = plant;
        this.soul = soul;
        this.characterName = name;
        this.background = background;
        this.sprite = sprite;
    }

    public Character(string name, int background, int quality)
    {
        //base on quality and background random generate a character
        this.characterName = name;
        this.background = background;

    }
}
