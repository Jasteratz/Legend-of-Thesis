using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;

    public Vector2 burst = new Vector2(2, 0);

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    private Vector3 change;

    public float runSpeed = 20.0f;

    public AudioSource Steps;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        if(animator.GetBool("moving").Equals(true))
        {
            Debug.Log("mpainei");
            if(!Steps.isPlaying)
            Steps.Play();
        }
        else
        {
            Steps.Stop();
        }
    }

    void FixedUpdate()
    {
     
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal"); 
        change.y = Input.GetAxisRaw("Vertical");
        
        if (change.x != 0 && change.y != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                change.x *= moveLimiter;
                change.y *= moveLimiter;
            }
        animator.SetFloat("moveX", change.x);
        animator.SetFloat("moveY", change.y);
        animator.SetBool("moving", true);
        body.MovePosition(transform.position + change * runSpeed * Time.deltaTime);
        if (change==Vector3.zero) animator.SetBool("moving", false);

    }

    public void Force()
    {
        Debug.Log("mpainei sto event");
        body.AddForce(burst, ForceMode2D.Force);
    }
}
