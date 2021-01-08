using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int background;

    //property
    public int[] stats;
    //  air water fire ground plant soul

    //sprite
    public Sprite sprite;
    
    //item
    public int[] itemlist;

    public Character(string name, int background, int[] stats,Sprite sprite)
    {
        this.stats = stats;
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
