using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Attack"))
        {
            animator.SetBool("Attacking", true);
        } else
        {
            animator.SetBool("Attacking", false);
        }
    }
}
