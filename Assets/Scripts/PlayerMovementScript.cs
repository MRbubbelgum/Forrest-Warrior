using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    public CharacterController2D controller;
    public PlayerCombat combatScript;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    public bool jump = false;
    public bool crouch = false;
    public bool falling = false;
    public Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            crouch = false;
            animator.SetBool("IsJumping", true);
            runSpeed = 8f;


        }

        if(Input.GetButtonDown("Crouch"))
        { 
            crouch = true;
        }

        else if (Input.GetButtonUp("Crouch"))
        {
             crouch = false;
        }
        
        if(rb.velocity.y < 0)
        {
            falling = true;
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }

    }
    public void OnLanding ()
    {
        
        animator.SetBool("IsFalling", false);
        falling = false;

    }

    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    public void NotCrouching()
    {
        crouch = false;
    }
    public void ContinueCrouching()
    {  
        if (Input.GetButton("Crouch"))
        {
          crouch = true;
        }
        
    }

}
