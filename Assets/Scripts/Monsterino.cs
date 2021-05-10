using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsterino : Enemy

    
{
    
    
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    //public Animator anim;
    


    // Start is called before the first frame update
    void Start(){
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;//Detect the transform of the object with the player tag
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate(){

        CheckDistance();
    }

    void CheckDistance() {
        if(Vector3.Distance(target.position,transform.position)<= chaseRadius //Chase the character
           && Vector3.Distance(target.position ,transform.position)>attackRadius)//But only while not in attack radius (prevents the collision between the center point of log and the collider of the player

        {
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState!=EnemyState.stagger && currentState != EnemyState.dead)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp",true);
                
            }
            else if (currentState == EnemyState.stagger)
            {
                changeAnim(Vector3.zero);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) < attackRadius && currentState!=EnemyState.stagger)
        {
            
            StartCoroutine(AttackCo(myRigidBody));
        }
        else
        {
            
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveY", 0);
            anim.SetBool("wakeUp", false);
            myRigidBody.velocity = Vector2.zero;
        }
    }

    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                setAnimFloat(Vector2.right);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.x < 0)
            {
                setAnimFloat(Vector2.left);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                setAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                setAnimFloat(Vector2.down);
            }
        }
    }
    
    private void setAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);

    }
    
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    
}
