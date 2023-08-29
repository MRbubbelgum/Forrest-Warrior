using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public Transform dashAttackPoint;
    public float attackRange = 0.5f;
    public float dashAttackRange = 0.7f;
    public LayerMask enemyLayers;
    public PlayerMovementScript playerMovementScript;
    public CharacterController2D mainCharacterScript;
    public Enemy enemyScript;
    public bool jump = false;
    public string IJ = "IsJumping";

    private bool IsAttacking = false;
    private bool IsDashAttacking = false;
    public float dashForce = 10f;
    private Rigidbody2D rb;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    



    public int attackDamage = 30;
    public int dashAttackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float dashAttackRate = 1f;
    float nextDashAttackTime = 0f;

    public float staggerTime = 2f;

    private void Awake()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        rb = GetComponent<Rigidbody2D>();
        mainCharacterScript = GetComponent<CharacterController2D>(); 
        playerMovementScript = GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                if (animator.GetBool("IsJumping"))
                {
                    animator.SetBool("IsJumping", false);
                }
                animator.SetTrigger("IsAttacking");
                nextAttackTime = Time.time + 1f / attackRate;
                nextDashAttackTime = Time.time + 1f / attackRate;

                if (mainCharacterScript.m_Grounded == true)
                {
                    playerMovementScript.runSpeed = 0f;
                }

                while (Time.time >= nextAttackTime)
                {
                    playerMovementScript.runSpeed = 0;
                }

            }
        }
        if (Time.time >= nextDashAttackTime)
        {
            if ((mainCharacterScript.m_Grounded == true && !animator.GetBool("IsCrouching") && playerMovementScript.crouch == false && playerMovementScript.jump == false && !animator.GetBool("IsJumping")))
            {
                if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Mouse1)))
                {
                    return;
                }
                else if ((Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Mouse1)) || (Input.GetKey(KeyCode.D) && (Input.GetKeyDown(KeyCode.Mouse1))))
                {
                    animator.SetTrigger("IsDashAttacking");
                    playerMovementScript.runSpeed = 0f;

                    nextDashAttackTime = Time.time + 1f / dashAttackRate;
                    nextAttackTime = Time.time + 2f / attackRate;
                } 
            }
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }
        }
    }
    public void EndAttack()
    {
        IsAttacking = false;
        if (!IsAttacking)
        {
            playerMovementScript.runSpeed = 8f;
        }
    }

    void DashAttack()
    {
        if (!IsDashAttacking)
        {
            IsDashAttacking = true;

            

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, dashAttackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(dashAttackDamage);
                }
            }


            if (Input.GetKey(KeyCode.D))
                {
                    Vector3 dashDirection = transform.right.normalized;

                    // Apply a force to simulate the dash
                    rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);

                    // Stop dashing after a delay (adjust this value as needed)
                    StartCoroutine(StopDashingAfterDelay(0.5f));
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    Vector3 dashDirection = -transform.right.normalized;

                    // Apply a force to simulate the dash
                    rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);

                    // Stop dashing after a delay (adjust this value as needed)
                    StartCoroutine(StopDashingAfterDelay(0.5f));
                }
                else if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
                {
                return;
                }
        }
        
        
        
    }
    public void EndDashAttack()
    {
        IsDashAttacking = false;
        if(!IsDashAttacking && playerMovementScript.runSpeed == 0f)
        {
            playerMovementScript.runSpeed = 8f;
        }


    }
    private System.Collections.IEnumerator StopDashingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsDashAttacking = false;

    }

        void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamageSelf(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        healthBar.SetHealth(currentHealth);


        if (currentHealth <= 0)
        {
            DieSelf();

        }
    }
    void DieSelf()
    {
        animator.SetBool("Death", true);

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<PlayerMovementScript>().enabled = false;
        mainCharacterScript.enabled = false;
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        this.enabled = false;
        

    }
}
