using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private Animator animator;



    private Vector3 moveDirection;

    private CharacterController controller;

    private void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        animator = transform.GetComponentInChildren<Animator>();
        
    }
    void Update()
    {
        actionMove();
    }

    private void actionMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = transform.TransformDirection(moveX, 0, moveZ);
        if(moveDirection !=Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= walkSpeed;
            animator.SetFloat("Speed", 0.5f);
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= runSpeed;
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        controller.Move(moveDirection*Time.deltaTime);

    }
}
