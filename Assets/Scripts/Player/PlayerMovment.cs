using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {
    [SerializeField] private float movement_speed = 10.0f;
    private Rigidbody2D rb2D;
    private Vector2 movement;

    private void Start() {
        rb2D = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate() {
        moveCharacter(movement);
    }
    
    void moveCharacter(Vector2 movement_direction){
        rb2D.MovePosition((Vector2)transform.position + (movement_speed * 
                                                     movement_direction * 
                                                        Time.deltaTime));
    }

}
