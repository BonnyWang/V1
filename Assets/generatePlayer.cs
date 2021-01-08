using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;

public class generatePlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Character> characters;
    GameObject grid;
    public Sprite[] sprite;
    public GameObject[] players;

    Vector3Int cellPosition;

    void Start()
    {
        //test
        characters.Add(new Character("Bonny",1, 1, 2, 3, 4, 5, 6, sprite[0]));
        characters.Add(new Character("Bonny2", 1, 10, 32, 33, 41, 54, 66, sprite[1]));

        grid = GameObject.Find("Grid");
        players = new GameObject[characters.Count];
        for (int i = 0; i < characters.Count; i++)
        {
            createPrefab(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Character a = new Character();
    }


    public void createPrefab(int i)
    {
        //placingCharacter(x，y);
        Debug.Log(Mathf.RoundToInt(5 + (i - characters.Count / 2)));
        cellPosition = new Vector3Int(Mathf.RoundToInt(10+(i- characters.Count/2)), 5, 0);
        Debug.Log(cellPosition);
        Vector3 newPos = grid.GetComponent<Grid>().CellToWorld(cellPosition);
        Debug.Log(newPos);
        var newcharacter = Instantiate(playerPrefab, newPos, Quaternion.identity, grid.transform);
        newcharacter.name=characters[i].characterName;
        players[i] = newcharacter;
        //change sprite
        newcharacter.GetComponent<SpriteRenderer>().sprite = sprite[i];

        //initialize stat
        CharacterStats CS = newcharacter.GetComponent<CharacterStats>();
        CS.initializeProperty(characters[i].characterName, characters[i].air, characters[i].water, characters[i].fire, characters[i].ground, characters[i].plant, characters[i].soul);

    }

    public void placingCharacter()
    {
        //Vector3Int cellPosition;
        //cellPosition = new Vector3Int(y, x, 0);

        //placing character(should be written at generate player)
        //transform.position = gridLayout.CellToWorld(cellPosition);
    }


}
