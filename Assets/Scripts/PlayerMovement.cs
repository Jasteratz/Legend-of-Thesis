﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack,
    interact,
    stagger,
    idle,
    dash,
    death
}

public class PlayerMovement : MonoBehaviour {
    //Basics
    private Animator animator;
    public PlayerState currentState;
    private Rigidbody2D myRigidbody;

    //Movement
    public float speed;
    private Vector3 change;
    

    
    //Flip
    public bool facingRight = true;

    //Dungeon
    public DungeonLoader dungeon;

    public Boolean isDead=false;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        currentState = PlayerState.walk;
        
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        else
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if (change.x == 0 && change.y == 0 && currentState != PlayerState.attack)
            {
                currentState = PlayerState.idle;
                myRigidbody.velocity = new Vector2(0, 0);
            }
            else
                currentState = PlayerState.walk;


            if (currentState == PlayerState.walk || currentState == PlayerState.idle)
            {
                //TEST EDO TO FLIP
                if (change.x > 0 && !facingRight)
                    Flip();
                else if (change.x < 0 && facingRight)
                    Flip();
                UpdateAnimationAndMove();
            }
        }
    }

   

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    void Dash()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * 2 * Time.deltaTime
        );
        currentState = PlayerState.walk;
    }



    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("1"))
        {
            dungeon.LoadNextLevel();
            Debug.Log("epitelous");
        }
            
    }
}