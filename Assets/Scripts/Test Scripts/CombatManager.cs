using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour {
    public static CombatManager instance;

    public bool canReceiveInput;
    public bool inputReceived;
    public PlayerMovement mChar;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public float damage;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
    }

    public void Attack()
    {
       
        if (Input.GetButtonDown("Attack"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
            if (canReceiveInput&&mChar.currentState!=PlayerState.walk)
            {
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    //enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    enemiesToDamage[i].GetComponent<Enemy>().Knock(enemiesToDamage[i].GetComponent<Enemy>().myRigidBody, 1, 10f);
                }
                mChar.currentState = PlayerState.attack;
                inputReceived = true;
                canReceiveInput = false;
            }
            else
            {
                return;
            }
        }
    }


    public void InputManager()
    {
        if (!canReceiveInput) canReceiveInput = true;
        else canReceiveInput = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
