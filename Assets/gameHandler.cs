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
    public Text turnText;
    public List<GameObject> speedrank;
    [SerializeField] Button restartButton;
    void Start()
    {
        turnNum = 0;
        roundNum = 1;
        restartButton = GameObject.Find("Button").GetComponent<Button>();
        restartButton.onClick.AddListener(next);
        GP = GameObject.Find("generatePlayer").GetComponent<generatePlayer>();
        roundText = GameObject.Find("Round").GetComponent<Text>();
        turnText = GameObject.Find("Turns").GetComponent<Text>();

        players = GP.players;
        Rank();
        speedrank[0].GetComponent<characterControl>().urTurn = true;
        turnText.text = string.Format("{0} turns", speedrank[0].GetComponent<CharacterStats>().characterName);
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void next()
    {
        speedrank[turnNum].GetComponent<characterControl>().urTurn = false;
        speedrank[turnNum].GetComponent<characterControl>().availablePoint = 10;
        speedrank[turnNum].GetComponent<characterControl>().rMap.ClearAllTiles();

        turnNum++;
        Debug.Log(("turn",turnNum));
        
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
