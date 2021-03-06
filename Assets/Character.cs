﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int background;

    //property
    public int[] stats;
    public List<Skill>  skillList;
    //  air water fire ground plant soul

    //sprite
    public Sprite sprite;
    
    //item
    public int[] itemlist;

    public Character(string name, int background, int[] stats,Sprite sprite, List<Skill>  skill)
    {
        this.stats = stats;
        this.characterName = name;
        this.background = background;
        this.sprite = sprite;
        this.skillList = skill;
    }

    public Character(string name, int background, int quality)
    {
        //base on quality and background random generate a character
        this.characterName = name;
        this.background = background;

    }
}
