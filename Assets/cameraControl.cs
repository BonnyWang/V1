using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour{
    float hMove;
    float vMove;
    [SerializeField] float edgeSize = 10f;
    [SerializeField] float step;
    [SerializeField] float zoomstep;
    [SerializeField] float zoom = 80f;

    private Vector3 cameraFollowPosition;


    void Start(){
        transform.position = new Vector3(8.5f, 8.5f, -10);
    }

    void Update(){
        hMove = Input.GetAxis("Horizontal")*step;
        vMove = Input.GetAxis("Vertical")*step;
        //mousehandle();
        zoomScroll();


        if ( Mathf.Abs(hMove) > 0 || Mathf.Abs(vMove) > 0){
            //print(transform.position.x + hMove);
            //print("and");
            //print(transform.position.y + vMove);
            if (transform.position.x+hMove > 4 && transform.position.x + hMove < 12.5)
            {
                if(transform.position.y + vMove > 1.5 && transform.position.y + vMove < 17.5)
                {
                    
                    transform.Translate(hMove, vMove, 0f);
                }
            }
            //transform.Translate(hMove, vMove, 0f);
        }
    }

    private void zoomScroll()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            zoom -= zoomstep * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            zoom += zoomstep * Time.deltaTime;

            Debug.Log(zoom);
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomstep * Time.deltaTime*10;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomstep * Time.deltaTime*10;
        }
        zoom = Mathf.Clamp(zoom, 2.5f, 5f);
        Camera.main.orthographicSize = zoom;
    }


    private void mousehandle()
    {   
        //if true setting bound
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            hMove = step/3;
        }

        if (Input.mousePosition.x < edgeSize)
        {
            hMove = -step/3;
        }

        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            vMove = step/3;
        }

        if (Input.mousePosition.y < edgeSize)
        {
            vMove = -step/3;
        }
    }
}
