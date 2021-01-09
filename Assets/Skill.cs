using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Skill : MonoBehaviour
{
    public string skillName;
    public int maxDamage;
    public int ap;
    public int minDamage;
    public float acc;
    public int range;
    public skillList SL;
    public GameObject user;

    private void Start()
    {
       
    }



    public Skill(string name, int max,int min, float acc, int range,int ap)
    {
        this.skillName = name;
        this.minDamage = min;
        this.maxDamage = max;
        this.acc = acc;
        this.range = range;
        this.ap = ap;
        //this.user = user;
    }
}
