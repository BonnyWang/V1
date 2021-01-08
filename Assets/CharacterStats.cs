using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{


    public string characterName;
    public int air;
    public int water;
    public int fire;
    public int ground;
    public int plant;
    public int soul;

    public int[] itemlist;

    //fight property
    public int skillAtk;
    public int meleeAtk;
    public int skillDef;
    public int stealth;
    public int range;
    public int acc;
    public int meleeDef;
    public int strength;
    public int view;
    public int morale;
    public int hp;
    public int ap;
    public int speed;



    void calculateProperty()
    {   

        //check through effect and update the propety

    }

    public void initializeProperty(string name,int air, int water, int fire, int ground, int plant, int soul)
    {
        //initialize property with itemlist and six stats
        this.air = air;
        this.water = water;
        this.fire = fire;
        this.ground = ground;
        this.plant = plant;
        this.soul = soul;
        this.characterName = name;

        //calculation of different Stats

    }
}
