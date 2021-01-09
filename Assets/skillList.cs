using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class skillList : MonoBehaviour
{


    public bool isSkilling =false;
    public bool dontMove = false;
    public int minDamage = 0;
    public int maxDamage = 0;
    public float skillAcc = 1;
    public gameHandler GH;
    public tilegenerate TG;
    public GameObject user;
    public CharacterStats userStats;
    public GameObject target;
    public List<GameObject> players;
    public int[,] skillMap;
    public Skill skillUsing;
    public GridLayout gridLayout;
    public Text apText;
    // Start is called before the first frame update
    void Start()
    {
        GH = GetComponent<gameHandler>();
        players = GH.players;

    }

    // Update is called once per frame
    void Update()
    {



        if (isSkilling)
        {
            apText.text = string.Format("AP: {0}-{1}", user.GetComponent<characterControl>().availablePoint, skillUsing.ap);
            if (Input.GetButtonDown("Click") && EventSystem.current.IsPointerOverGameObject())
            {
                //do nothing since it click on UI;
            }

            else if (Input.GetButtonDown("Click"))
            {
                Vector3Int tempPos = gridLayout.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Debug.Log(tempPos);

                //ap cost not enough
                if (user.GetComponent<characterControl>().availablePoint < skillUsing.ap)
                {
                    Debug.Log("no ap");
                }

                //press on target
                else if (skillMap[tempPos.y, tempPos.x] >1)
                {
                    Debug.Log("effect!");
                    target = players[skillMap[tempPos.y, tempPos.x] - 2];
                    effectSkill();
                    reClear();
                }


            }

            else if(Input.GetButtonDown("LeftClick"))
            {
                Debug.Log("HERE");
                reClear();
            }
        }
    }

    public void useSkill(Skill a, GameObject b)
    {
        
        skillUsing = a;
        user = b;
        TG.updateSkill(singleEnemyTarget(a.range));
        isSkilling = true;
        
        //update ap text(after isSkilling ture cuz CC take it into consideration



    }

    int effectSkill()
    {
        
        CharacterStats UCS = user.GetComponent<CharacterStats>();
        CharacterStats TCS = target.GetComponent<CharacterStats>();

        //-ap
        user.GetComponent<characterControl>().availablePoint -= skillUsing.ap;
        apText.text = string.Format("AP: {0}", user.GetComponent<characterControl>().availablePoint.ToString());
        Debug.Log(string.Format("used {0} ap", skillUsing.ap));
        
        //miss or not
        float hit = Random.Range(0, 1);
        if (skillUsing.acc * (1 - TCS.stealth + UCS.acc) <  hit)
        {
            Debug.Log(skillUsing.acc);
            Debug.Log(TCS.stealth);
            Debug.Log(UCS.acc);
            Debug.Log("miss!!!!!!!!");
            return 0;
        }

        else
        {
            float Rhit = Random.Range(skillUsing.minDamage, skillUsing.maxDamage);
            //need type addition on damage as well;
            int damage = Mathf.RoundToInt(Rhit * (UCS.skillAtk)*(1-TCS.skillDef));
            Debug.Log(string.Format("damage is {0}", damage));
            TCS.hpLeft = TCS.hpLeft - damage;


        }



        return 0;
    }

    public int[,] singleEnemyTarget(int range)
    {
        skillMap = new int[TG.tmpSize.x, TG.tmpSize.y];
        //turn co to 1 and turn range with target on it to 2
        for (int i = 0; i < TG.tmpSize.x; i++)
        {
            for (int j = 0; j < TG.tmpSize.y; j++)
            {
                skillMap[i, j] = 0;
            }
        }

        int x = user.GetComponent<characterControl>().cellPosition.y;
        int y = user.GetComponent<characterControl>().cellPosition.x;
        int vp = range+1;
        //Debug.Log(x);
        //Debug.Log(y);
        //Debug.Log(vp);
        int k = 0;

        for (int j = 0; j < vp; j++)
        {
            if (((x + j - 1) % 2) == 1 && j > 0)
            {
                k++;
            }
            for (int i = 0; i < vp * 2 - 1; i++)
            {

                if (i > vp * 2 - 2 - j )
                {
                    //do nothing
                }
                else if(j==0 && k+i+1 == vp)
                {
                    //do nothing for its own coordinate
                }
                else if (y - vp + k + i + 1 < TG.tmpSize.y && y - vp + k + i + 1 > -1)
                {
                    //Debug.Log(string.Format("({0},{1})  ready", x + j, y - vp + k + i - j));
                    if (x + j < TG.tmpSize.x)
                    {
                        skillMap[x + j, y - vp + k + i + 1] = checkTarget(x + j, y - vp + k + i + 1);
                        //Debug.Log(string.Format("({0},{1})  cleared", x + j, y - vp + k + i + 1));
                    }
                    if (x - j > -1)
                    {
                        skillMap[x - j, y - vp + k + i + 1] = checkTarget(x - j, y - vp + k + i + 1);
                        //Debug.Log(string.Format("({0},{1})  cleared", x - j, y - vp + k + i + 1));
                    }
                }
            }
        }




        return skillMap;
    }

    int checkTarget(int y, int x)
    {
        
        for(int i = 0; i < players.Count; i++)
        {

            int tx = players[i].GetComponent<characterControl>().cellPosition.x;
            int ty = players[i].GetComponent<characterControl>().cellPosition.y;
            if (tx == x && y == ty)
            {
                return (2+i);
            }
        }
        return 1;

    }

    public void reClear()
    {
        //when on skilling press another skill or walk
        TG.resetSMap();
        skillUsing = null;
        skillMap = null;
        target = null;
        user = null;
        isSkilling = false;
        dontMove = true;
    }
}
