using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy

    
{
    private Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    


    // Start is called before the first frame update
    void Start(){
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;//Detect the transform of the object with the player tag
        
        
    }

    // Update is called once per frame
    void FixedUpdate(){

        CheckDistance();
    }

    void CheckDistance() {
        if(Vector3.Distance(target.position,transform.position)<= chaseRadius //Chase the character
                            && Vector3.Distance(target.position ,transform.position)>attackRadius)//But only while not in attack radius (prevents the collision between the center point of log and the collider of the player

        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState!=EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        
        
    }
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    
}
