using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour

{
    private Animator animator;
    public int health = 100;
    public Slider HP;



    public PlayerMovement canMove;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canMove = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Death()
    {
        animator.SetTrigger("Pethanes");
        canMove.isDead = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);

    }
    public void getDamaged(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            HP.value -= damage;

        }
        else 
        {
            StartCoroutine(Death());
        }
    }
}
