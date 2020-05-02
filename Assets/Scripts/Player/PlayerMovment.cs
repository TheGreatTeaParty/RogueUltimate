using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float move_speed;

    private Vector3 movement;
    private Rigidbody2D body;

    //Add Rigidbody for the future, there is no need in it right now
    void Start()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

   
    void Update()
    {
        //Make direction 0,0,0 if user do not input
        movement = new Vector3(0f, 0f, 0f);

        //Checking for user input and change direction;
        if (Input.GetKey(KeyCode.A))
        {
            movement = new Vector3(-1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement = new Vector3(1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement = new Vector3(0f, 1f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement = new Vector3(0f, -1f, 0);
        }

        //Tranform the position of the player
        transform.position += movement * move_speed * Time.deltaTime;
    }
}
