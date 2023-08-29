using Cainos.LucidEditor;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public HealthBar healthBar;
    public int maxHealth = 100;
    int currentHealth;
    public BorderScript border;
    public SpriteRenderer sp;



    public GameObject pointA;
    public GameObject pointB;
    public  GameObject MainCharacter;
    public Rigidbody2D rb;
    private Transform currentPoint;
    [SerializeField] private string mainCharacter = "MainCharacter";
    [SerializeField] private string borderScript = "Border Manager";


    public float walkSpeed = 0;
    public float chaseSpeed = 0;
    public bool e_FacingRight = true;

    public bool IsEnemyAttacking = false;
    public Transform EnemyAttackPoint;
    public float attackRange = 0.5f;
    public LayerMask mainCharacterLayer;
    public int enemyAttackDamage = 20;
    public float attackRate = 2f;
    float nextEnemyAttackTime = 0f;
    private bool isWaiting = false;
    public bool isEnemyChasing = false;







    // Start is called before the first frame update
    void Start()
    {
        MainCharacter = GameObject.Find(mainCharacter);
        border = GameObject.FindGameObjectWithTag("Border").GetComponent<BorderScript>();
        healthBar = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<HealthBar>();
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentPoint = pointB.transform;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    public void Die()
    {

        animator.SetBool("IsDead", true);


        border.removeBorder();

        GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        this.enabled = false;
    }

    public void Eflip()
    {
        if (rb.velocity.x > 0 && e_FacingRight)
        {
            e_FacingRight = false;
            Vector2 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x); // Make sure it's positive
            transform.localScale = theScale;
        }
        else if (rb.velocity.x < 0 && !e_FacingRight)
        {
            e_FacingRight = true;
            Vector2 theScale = transform.localScale;
            theScale.x = -Mathf.Abs(theScale.x); // Make sure it's negative
            transform.localScale = theScale;
        }
    }
    public void EnemyAttack()
    {
        Collider2D[] hitMainCharacter = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, mainCharacterLayer);


        foreach (Collider2D MainCharacter in hitMainCharacter)
        {
            MainCharacter.GetComponent<PlayerCombat>().TakeDamageSelf(enemyAttackDamage);
        }
    }


    public void EndEnemyAttack()
    {
        IsEnemyAttacking = false;

        animator.SetBool("IsEnemyAttacking", false);
        GetComponent<SpriteRenderer>().color = new Color(215f, 255f, 236f, 0.055f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawWireSphere(EnemyAttackPoint.position, attackRange);
    }


    IEnumerator WaitThenChangeDirection()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }

    public void EnemyGetColor()
    {
       GetComponent<SpriteRenderer>().color = new Color(215f, 255f, 236f, 1f);
    }



// Update is called once per frame
void Update()
    {
        if(currentHealth == 100)
        {
            healthBar.fillTransparent();
            border.borderInvisible();
        }
        else
        {
            healthBar.fillColor();
            border.borderVisible();
        }
        
      
            if (isEnemyChasing == true)
            {
                if (Time.time >= nextEnemyAttackTime)
                {
                    if (IsEnemyAttacking == true)
                    {
                        
                        rb.velocity = new Vector2(0, 0);
                        animator.SetBool("IsEnemyAttacking", true);
                        nextEnemyAttackTime = Time.time + 0.5f / attackRate;

                    }
                    else if (IsEnemyAttacking == false)
                    {
                        Vector2 direction = (MainCharacter.transform.position - transform.position).normalized;
                        direction.y = 0;
                        rb.velocity = direction.normalized * chaseSpeed;

                        if (rb.velocity.x > 0 && e_FacingRight)
                        {
                            Eflip();
                        }
                        else if (rb.velocity.x < 0 && !e_FacingRight)
                        {
                            Eflip();
                        }

                        animator.SetFloat("isWalking", Mathf.Abs(walkSpeed));
                    }
                }           
            }

            else if (!isWaiting)
                {
                    Vector2 point = currentPoint.position - transform.position;
                    if (currentPoint == pointB.transform)
                    {

                        rb.velocity = new Vector2(walkSpeed, 0);

                    }
                    else
                    {
                        rb.velocity = new Vector2(-walkSpeed, 0);

                    }
                    if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
                    {
                        currentPoint = pointA.transform;
                        StartCoroutine(WaitThenChangeDirection());

                    }
                    if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
                    {
                        currentPoint = pointB.transform;
                        StartCoroutine(WaitThenChangeDirection());
                    }

                    if (rb.velocity.x > 0 && e_FacingRight)
                    {
                        Eflip();
                    }
                    else if (rb.velocity.x < 0 && !e_FacingRight)
                    {
                        Eflip();
                    }
                    animator.SetFloat("isWalking", Mathf.Abs(walkSpeed));

                    if (Time.time >= nextEnemyAttackTime)
                    {
                        if (IsEnemyAttacking == true)
                        {
                            
                            rb.velocity = new Vector2(0, 0);
                            animator.SetBool("IsEnemyAttacking", true);
                            nextEnemyAttackTime = Time.time + 0.5f / attackRate;
                            IsEnemyAttacking = false;
                }
                    }
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    animator.SetFloat("isWalking", 0);

                    if (Time.time >= nextEnemyAttackTime)
                    {
                        if (IsEnemyAttacking == true)
                        {
                            
                            rb.velocity = new Vector2(0, 0);
                            animator.SetBool("IsEnemyAttacking", true);
                            nextEnemyAttackTime = Time.time + 0.5f / attackRate;
                        }
                    }
                }
        


    }
}
