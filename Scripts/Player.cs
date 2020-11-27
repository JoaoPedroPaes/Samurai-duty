using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Vector2 movement;
    public Rigidbody2D rb;
    public Animator animator; 
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float maxHealth = 100f;
    public float currentHealth;
    public float moveSpeed = 5f;
    public float attackRange = 0.5f;
    public float attackDamage = 50f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    
    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(Time.time >= nextAttackTime){
        if(Input.GetKeyDown(KeyCode.Z)){
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamege(attackDamage);

        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



   void ReciveDamage(float damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hiter");

        if(currentHealth <=0){
            Die();
        }
    }


   void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy"){
            ReciveDamage(10f); 
            rb.AddForce(new Vector2(10f, 0.5f), ForceMode2D.Impulse);
        }

    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        moveSpeed = 0f;
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }




}
