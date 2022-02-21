using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    private Vector3 movementInput;
    private Vector3 velocity;
    private float speed;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = 9.81f;


    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("isGrounded", true);
            velocity.y = -1f;
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpForce;
                animator.SetBool("Jumping", true);
                animator.SetBool("isGrounded", false);
            }
        } else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        if (movementInput.magnitude > 0.01f)
        {
            animator.SetBool("Moving", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementInput), Time.deltaTime * rotationSpeed);

            if (Input.GetButton("Walk"))
            {
                speed = walkSpeed;
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
                speed = runSpeed;
            }
        } else
        {
            animator.SetBool("Moving", false);
        }

        controller.Move(movementInput * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
