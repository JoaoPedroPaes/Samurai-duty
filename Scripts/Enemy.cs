using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 100f;
    float currentHealth;
    public float speed;
    public float timeDeath = 1f;
    public float collisionDamage = 20f;
    private Transform target;
    public Animator animator;


    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed* Time.deltaTime);
    }


    public void TakeDamege(float damage)
    {

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <=0){
            Die();
        }


    }


    void Die()
    {
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, timeDeath);
    }



    

}
