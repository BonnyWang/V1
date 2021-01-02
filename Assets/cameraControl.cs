using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour{
    float hMove;
    float vMove;
    [SerializeField] float step;
    void Start(){
        
    }

    void Update(){
        hMove = Input.GetAxis("Horizontal")*step;
        vMove = Input.GetAxis("Vertical")*step;

        if( Mathf.Abs(hMove) > 0 || Mathf.Abs(vMove) > 0){
            transform.Translate(hMove, vMove, 0f);
        }
    }
}
