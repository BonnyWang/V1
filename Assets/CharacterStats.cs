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
    public float skillAtk;
    public float meleeAtk;
    public float skillDef;
    public float stealth;
    public float range;
    public float acc;
    public float meleeDef;
    public float strength;
    public float view;
    public float morale;
    public float hp;
    public float ap;
    public float speed;



    void calculateProperty()
    {   

        //check through effect and update the propety

    }

    public void initializeProperty(string name,int[] stats)
    {
        //initialize property with itemlist and six stats
        this.air = stats[0];
        this.water = stats[1];
        this.fire = stats[2];
        this.ground = stats[3];
        this.plant = stats[4];
        this.soul = stats[5];
        this.characterName = name;

        //calculation of different Stats
        //fire(meleeAtk and skillAtk)
        if (fire >= 0)
        {   
            //0.2 not 0.25 due to the amount lowered
            meleeAtk = (75-0.2f*fire) / 75;
            skillAtk = (100 + fire) / 100;
        }
        else
        {
            meleeAtk = (75 - fire) / 75;
            skillAtk = (100 + 0.25f*fire) / 100;
        }

        //water(skillDef and stealth)
        if (water >= 0)
        {
            skillDef = water / (water + 100f);
            stealth = -water * 0.1f;
        }
        else
        {
            skillDef = 0.25f*water / (0.25f*water + 100f);
            stealth = -water * 0.5f;
        }

        //air range and acc
        if (air >= 0)
        {
            range = 1 + 0.01f * air;
            acc = -air * 0.08f;
        }
        else
        {
            range = 1 - 0.0025f * air;
            acc = -air * 0.4f;
        }

        //ground meleeDef and strength
        if (ground >= 0)
        {
            meleeDef = ground / (ground + 100f);
            strength = -ground * 0.12f;
        }
        else
        {
            meleeDef = 0.25f * water / (0.25f * water + 100f);
            strength = -ground * 0.6f;
        }

        //plant health and speed
        speed = (plant + 100f) / 2f;
        if (plant >= 0)
        {
            hp = 100f + plant;
        }
        else
        {
            hp = 100f + 0.25f * plant;
        }
        

        //soul  view and morale
        if (soul >= 0)
        {
            view = 5 + soul * 0.05f;
            morale = 50 - 0.25f * soul;
        }
        else
        {
            view = 5 + soul * 0.02f;
            morale = 50 - soul * 0.5f;
        }
    }
}
