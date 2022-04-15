using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    Vector3 newPosition;
    public float moveSpeed = 5;
    public float jumpSpeed = 35;

    public LayerMask ground;

    //Rigidbody仮以
    Rigidbody2D rig;
    CapsuleCollider2D col;
    Animator ani;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        ani = GetComponent<Animator>();
        if (col == null)
            col = this.gameObject.AddComponent<CapsuleCollider2D>();
        if (rig == null)
            rig = this.gameObject.AddComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        move();
        switchAni();
    }


    //随詳よΑ仮以
    private void move() {
        float dir = Input.GetAxis("Horizontal");
        if(dir != 0)
            ani.SetFloat("running", Mathf.Abs(dir)); 
        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
            rig.AddForce(new Vector2(dir * moveSpeed, 0), ForceMode2D.Force);
        if (Input.GetKeyDown(KeyCode.K)) {
            rig.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            ani.SetBool("isJump", true); 
        }
        if (dir < 0) 
            this.transform.localScale = new Vector3(-1, 1, 1);
        else if (dir > 0)
            this.transform.localScale = new Vector3(1, 1, 1);
        
    }

    void switchAni() {
        if (ani.GetBool("isJump")) {
            
        }
        else if (ani.GetBool("isFall")) {
            if (rig.velocity.y == 0) {
                ani.SetBool("isFall", false);
            }
        }
        if (rig.velocity.y < -0.2f) {
            ani.SetBool("isJump", false);
            ani.SetBool("isFall", true);
        }
    }
}


