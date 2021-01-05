using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour{
    Vector3 destPosition;
    Vector3 a;
    Vector3 tempPosition;
    [SerializeField] float step;
    [SerializeField] int APRange;
    bool canMove;
    int availablePoint;
    GridLayout gridLayout;
    [SerializeField] Text apText;
    [SerializeField] Button restartButton;
    // Start is called before the first frame update
    void Start(){
        destPosition = transform.position;
        tempPosition = transform.position;
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        gernerateAP();
        canMove = true;

        restartButton.onClick.AddListener(restartTurn);
       
    }

    // Update is called once per frame
    void Update(){
        if(canMove){
            if(Input.GetButtonDown("Click")){
                 a = Input.mousePosition;

                destPosition = Camera.main.ScreenToWorldPoint(a);
                print(destPosition.x);
                print(destPosition.y);
                print(a.x);
                print(a.y);
            }
        
            tempPosition = Vector2.MoveTowards(tempPosition,destPosition, step);

            // convert continous to discrete, if same cell then no movement
            Vector3Int cellPosition = gridLayout.WorldToCell(tempPosition);
            if(transform.position != gridLayout.CellToWorld(cellPosition)){
                
                if(GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("highLand"))){
                    if((availablePoint - 2) >= 0){
                        availablePoint -= 2;
                        transform.position = gridLayout.CellToWorld(cellPosition);
                    }

                }else{
                    // normal place
                    transform.position = gridLayout.CellToWorld(cellPosition);
                    availablePoint--;
                }

                tempPosition = transform.position;
                updateAPtext();
            }
            if(availablePoint == 0){
                endTurn();
            }
        }
        
    }


    void gernerateAP(){
        availablePoint = Random.Range(1,APRange);
        apText.text = availablePoint.ToString();
    }

    void updateAPtext(){
        apText.text = availablePoint.ToString();
    }

    void restartTurn(){
        canMove = true;
        gernerateAP();
    }

    void endTurn(){
        canMove = false;
        destPosition = transform.position;
    }
}
