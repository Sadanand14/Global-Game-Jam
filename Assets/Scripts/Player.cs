using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    bool isMoving = false;
    public float speed;
    Vector2 direction;
    enum state {START, STOP,TRANS };
    public Sprite upidle,uptrans;
    public Sprite downidle,downtrans;
    public Sprite leftidle,lefttrans;
    public Sprite rightidle,righttrans;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        GetInput();
        if(isMoving)
        Movement.Move(transform,direction,speed);
        isMoving = false;
	}

    void GetInput()
    {
        Vector2 tmpVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            direction = Vector2.up;
            GetComponent<SpriteRenderer>().sprite = upidle;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            direction = Vector2.down;
            GetComponent<SpriteRenderer>().sprite = downidle;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            direction = Vector2.left;
            GetComponent<SpriteRenderer>().sprite = leftidle;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            direction = Vector2.right;
            GetComponent<SpriteRenderer>().sprite = rightidle;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }
}
