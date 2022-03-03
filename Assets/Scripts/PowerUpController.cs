using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    public GameObject powerUpEffect;
    public GameObject powerUpIcon;
    public float duration;
    [Space]
    public bool isDoubleJump;
    [Space]
    public bool isSpeedBoost;
    public float speedMultiplier;
    [Space]
    public bool isSizeIncrease;
    public float sizeMultiplier;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {     
        if (!player.GetComponent<CharacterControllerMovement>().hasPowerUp)
        {
            Instantiate(powerUpEffect, transform.position, transform.rotation);

            powerUpIcon.SetActive(true);

            if (isSpeedBoost)
            {
                // Apply effect
                player.GetComponent<CharacterControllerMovement>().runSpeed *= speedMultiplier;
                player.GetComponent<CharacterControllerMovement>().hasPowerUp = true;

                // Disable components for powerup
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // Wait for effect to run out
                yield return new WaitForSeconds(duration);

                // Remove effect
                player.GetComponent<CharacterControllerMovement>().runSpeed /= speedMultiplier;
                player.GetComponent<CharacterControllerMovement>().hasPowerUp = false;
            }

            if (isDoubleJump)
            {
                // Apply effect
                player.GetComponent<CharacterControllerMovement>().doubleJumpActive = true;
                player.GetComponent<CharacterControllerMovement>().hasPowerUp = true;

                // Disable components for powerup
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // Wait for effect to run out
                yield return new WaitForSeconds(duration);

                // Remove effect
                player.GetComponent<CharacterControllerMovement>().doubleJumpActive = false;
                player.GetComponent<CharacterControllerMovement>().hasPowerUp = false;
            }

            if (isSizeIncrease)
            {
                // Apply effect
                player.transform.localScale *= sizeMultiplier;
                player.GetComponent<CharacterControllerMovement>().hasPowerUp = true;

                // Disable components for powerup
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // Wait for effect to run out
                yield return new WaitForSeconds(duration);

                // Remove effect
                player.transform.localScale /= sizeMultiplier;
                player.GetComponent<CharacterControllerMovement>().hasPowerUp = false;
            }

            powerUpIcon.SetActive(false);
            Destroy(gameObject);
        }
    }
}
