using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class gameHandler : MonoBehaviour
{
    public int turnNum;
    public int roundNum;
    public generatePlayer GP;
    public List<GameObject> players;
    public Text roundText;
    public Text hpText;
    public Text turnText;
    public Text Fire;
    public Text Water;
    public Text Air;
    public Text Ground;
    public Text Soul;
    public Text Plant;
    public Image turnPlayer;
    public GameObject buttonPrefab;
    public GameObject Canvas;
    public List<GameObject> speedrank;
    public List<GameObject> buttonList;
    [SerializeField] Button restartButton;
    public skillList SL;
    void Start()
    {
        turnNum = 0;
        roundNum = 1;
        restartButton = GameObject.Find("Button").GetComponent<Button>();
        restartButton.onClick.AddListener(next);
        GP = GameObject.Find("generatePlayer").GetComponent<generatePlayer>();
        roundText = GameObject.Find("Round").GetComponent<Text>();
        turnText = GameObject.Find("Turns").GetComponent<Text>();
        hpText = GameObject.Find("HP").GetComponent<Text>();
        turnPlayer = GameObject.Find("Image").GetComponent<Image>();
        Fire = GameObject.Find("Fire").GetComponent<Text>();
        Water = GameObject.Find("Water").GetComponent<Text>();
        Air = GameObject.Find("Air").GetComponent<Text>();
        Plant = GameObject.Find("Plant").GetComponent<Text>();
        Soul = GameObject.Find("Soul").GetComponent<Text>();
        Ground = GameObject.Find("Ground").GetComponent<Text>();
        Canvas = GameObject.Find("Canvas");
        SL = GetComponent<skillList>();
        players = GP.players;
        Rank();
        speedrank[0].GetComponent<characterControl>().urTurn = true;
        turnText.text = string.Format("{0} turns", speedrank[0].GetComponent<CharacterStats>().characterName);
        hpText.text = string.Format("{0}/{1}", speedrank[0].GetComponent<CharacterStats>().hpLeft, speedrank[0].GetComponent<CharacterStats>().hp);
        Fire.text = string.Format("Fire {0}", speedrank[0].GetComponent<CharacterStats>().fire);
        Water.text = string.Format("Water {0}", speedrank[0].GetComponent<CharacterStats>().water);
        Ground.text = string.Format("Ground {0}", speedrank[0].GetComponent<CharacterStats>().ground);
        Soul.text = string.Format("Soul {0}", speedrank[0].GetComponent<CharacterStats>().soul);
        Plant.text = string.Format("Plant {0}", speedrank[0].GetComponent<CharacterStats>().plant);
        Air.text = string.Format("Air {0}", speedrank[0].GetComponent<CharacterStats>().air);
        turnPlayer.sprite = speedrank[0].GetComponent<CharacterStats>().sprite;

        //initialize skill list
        for(int i=0;i< speedrank[0].GetComponent<CharacterStats>().skilllist.Count; i++)
        {
            Vector3 newPos = new Vector3(0.75f*i, 0 , 0);
            var newSkillButton = Instantiate(buttonPrefab, Canvas.transform);
            //newSkillButton.transform.position = newPos;
            newSkillButton.transform.Translate(newPos);
            Skill a = speedrank[0].GetComponent<CharacterStats>().skilllist[i];
            newSkillButton.GetComponentInChildren<Text>().text = a.skillName;
            Debug.Log(string.Format("range is {0}", a.range));
            buttonList.Add(newSkillButton);
            newSkillButton.GetComponent<Button>().onClick.AddListener(delegate { useSkill(a,speedrank[0]); });
            newSkillButton.GetComponent<Button>().onClick.AddListener(delegate { speedrank[0].GetComponent<characterControl>().resetControl(); });
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void useSkill(Skill a, GameObject user)
    {
        Debug.Log(a.skillName);
        Debug.Log(string.Format("range is {0}", a.range));
        SL = GameObject.Find("gameHandler").GetComponent<skillList>();
        SL.useSkill(a, user);
    }

    public void next()
    {   

        //clear all button
        for(int i = 0; i < buttonList.Count; i++)
        {
            Destroy(buttonList[i]);
        }

        speedrank[turnNum].GetComponent<characterControl>().urTurn = false;
        speedrank[turnNum].GetComponent<characterControl>().availablePoint = 10;
        speedrank[turnNum].GetComponent<characterControl>().rMap.ClearAllTiles();
        //effect end turn

        turnNum++;
        //Debug.Log(("turn",turnNum));
        
        if (turnNum > players.Count - 1)
        {
            roundNum++;
            roundText.text = string.Format("round {0}", roundNum);
            //calculate all character stats
            Rank();
            turnNum = 0;
        }

        Debug.Log(speedrank[turnNum]);
        turnText.text = string.Format("{0} turns", speedrank[turnNum].GetComponent<CharacterStats>().characterName);
        hpText.text = string.Format("{0}/{1}", speedrank[turnNum].GetComponent<CharacterStats>().hpLeft, speedrank[turnNum].GetComponent<CharacterStats>().hp);
        turnPlayer.sprite = speedrank[turnNum].GetComponent<CharacterStats>().sprite;
        Fire.text = string.Format("Fire {0}", speedrank[turnNum].GetComponent<CharacterStats>().fire);
        Water.text = string.Format("Water {0}", speedrank[turnNum].GetComponent<CharacterStats>().water);
        Ground.text = string.Format("Ground {0}", speedrank[turnNum].GetComponent<CharacterStats>().ground);
        Soul.text = string.Format("Soul {0}", speedrank[turnNum].GetComponent<CharacterStats>().soul);
        Plant.text = string.Format("Plant {0}", speedrank[turnNum].GetComponent<CharacterStats>().plant);
        Air.text = string.Format("Air {0}", speedrank[turnNum].GetComponent<CharacterStats>().air);

        //generate skill button
        for (int i = 0; i < speedrank[turnNum].GetComponent<CharacterStats>().skilllist.Count; i++)
        {
            Vector3 newPos = new Vector3( 1.5f*i, 0, 0);
            var newSkillButton = Instantiate(buttonPrefab, Canvas.transform);
            //newSkillButton.transform.position = newPos;
            newSkillButton.transform.Translate(newPos);
            Skill a = speedrank[turnNum].GetComponent<CharacterStats>().skilllist[i];
            newSkillButton.GetComponentInChildren<Text>().text = a.skillName;
            Debug.Log(string.Format("range is {0}", a.range));
            buttonList.Add(newSkillButton);
            newSkillButton.GetComponent<Button>().onClick.AddListener(delegate { useSkill(a, speedrank[turnNum]); });
            newSkillButton.GetComponent<Button>().onClick.AddListener(delegate { speedrank[turnNum].GetComponent<characterControl>().resetControl(); });
        }
        speedrank[turnNum].GetComponent<characterControl>().urTurn = true;
    }

    void Rank()
    {   
        
        //should have rerank
        speedrank = players.OrderByDescending(players => players.GetComponent<CharacterStats>().speed).ToList();
        for (int i = 0; i < speedrank.Count; i++)
        {
            //Debug.Log(speedrank[i].GetComponent<CharacterStats>().characterName);
        }
    }
}
