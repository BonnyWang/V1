using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour{
    Vector3 destPosition;
    Vector3 tempPosition;
    [SerializeField] float step;
    GridLayout gridLayout;
    // Start is called before the first frame update
    void Start(){
        destPosition = transform.position;
        tempPosition = transform.position;
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
       
    }

    // Update is called once per frame
    void Update(){
        
        if(Input.GetButtonDown("Click")){
            destPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        tempPosition = Vector2.MoveTowards(tempPosition,destPosition, step);

        // convert continous to discrete, if same cell then no movement
        Vector3Int cellPosition = gridLayout.WorldToCell(tempPosition);
        transform.position = gridLayout.CellToWorld(cellPosition);
    
    }

   
}
