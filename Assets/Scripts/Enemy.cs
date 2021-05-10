using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    idle,
    walk,
    attack,
    stagger,
    dead
}

public class Enemy : MonoBehaviour {

    public EnemyState currentState;
    //public AudioSource hitSound;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public float timeToDie=1.2f;
    public float timeToStagger = 0.2f;
    public Transform target;
    public float knockBackPower=3;

    public Rigidbody2D myRigidBody;

    public Animator anim;

    private void Awake()
    {
        health = maxHealth.initialValue;
        
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }



    private void Update()
    {
        //if (health <= 0) Destroy(gameObject);
    }
     public void TakeDamage(float damage)
     {
         health -= damage;
         
     }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        TakeDamage(damage);
        if (health > 0)
        {
            StartCoroutine(KnockCo(myRigidbody, knockTime));
            //StartCoroutine(HitCo(myRigidbody));
        }
        else
        {
            StartCoroutine(DeathCo());
        }        
    }

    private IEnumerator DeathCo()
    {
        anim.SetTrigger("EnemyHit");
        anim.SetBool("EnemyDead",true);
            yield return new WaitForSeconds(2.017f);
            this.gameObject.SetActive(false);
    }

    /*private IEnumerator HitCo(Rigidbody2D myRigidbody)
    {
        if (myRigidbody != null)
        {
            
        }
    }*/
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {

            yield return new WaitForSeconds(0.5f);
            anim.SetTrigger("EnemyHit");
            Debug.Log("mpainei edo");
            myRigidbody.velocity = Vector2.zero;
            Vector2 direction = (target.transform.position - this.transform.position).normalized;
            
            myRigidBody.AddForce(-direction * knockBackPower*20);
        }
        yield return 0;
    }


    public IEnumerator AttackCo(Rigidbody2D myRigidbody)
    {
        if (myRigidbody != null)
        {
            currentState = EnemyState.idle;
            
            anim.SetTrigger("Attack");
            myRigidBody.velocity = Vector2.zero;
       
            


        }
        yield return 0;
    }
    /*public void TakeDamage(int damage)
    {
         
        health -= damage;
        anim.SetTrigger("EnemyHit");
        
        //Debug.Log("efage hit0");
    }*/
}