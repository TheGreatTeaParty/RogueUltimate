﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    /*I've not understood exactly yet, but dudes with the big dicks in the Internet 
    said that it's better to use [SerializeField]*/ 
    [SerializeField] private float movement_speed = 10.0f;
    private Rigidbody2D rb2D;
    private Vector2 movement;

    private void Start() {
        rb2D = this.GetComponent<Rigidbody2D>();
    }

    /*There we receive input information (WASD or arrows or mobile stick)*/
    void Update() {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    /*I called func. moveCharacter there, because FixedUpdate is better for physic detection*/
    void FixedUpdate() {
        moveCharacter(movement);
    }
    
    /*This is the main function which calculate Character's movement*/
    void moveCharacter(Vector2 movement_direction){
        rb2D.MovePosition((Vector2)transform.position + 
                                    (movement_speed * 
                                    movement_direction * 
                                        Time.deltaTime));
    }

}
